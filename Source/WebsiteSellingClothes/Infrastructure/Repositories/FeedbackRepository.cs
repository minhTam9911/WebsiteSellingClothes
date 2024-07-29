using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories;
public class FeedbackRepository : IFeedbackRepository
{
	private readonly AppDbContext appDbContext;

	public FeedbackRepository(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

	public async Task<int> DeleteAsync(int id, Guid userId)
	{
		var feedback = await appDbContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
		if (feedback == null) throw new BadHttpRequestException("Feed does not exist");
		if (feedback.User!.Id != userId) throw new UnauthorizedAccessException("Forbidden");
		appDbContext.Feedbacks.Remove(feedback);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Feedback>?> GetAllAsync()
	{
		return await appDbContext.Feedbacks.ToListAsync();
	}

	public async Task<PagedListDto<Feedback>?> GetAllForProductAsync(string code, FilterDto filter)
	{
		IQueryable<Feedback> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Feedbacks.Where(x => x.Product!.Code == code);
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Feedbacks.Where(x => x.Product!.Code == code 
					&& (x.Rating.ToString().Contains(filter.Keyword) || x.Comment.ToLower().Contains(filter.Keyword)) );
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Feedback).GetProperties();
			var property = propertyInfo.FirstOrDefault(x => x.Name.Equals(filter.SortColumn, StringComparison.OrdinalIgnoreCase));
			var orderBy = filter.IsDescending ? "descending" : "ascending";
			if (property != null)
			{
				orderQueryBuilder.Append($"{property.Name.ToString()} {orderBy}");
			}
			string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
			if (!string.IsNullOrWhiteSpace(orderQuery))
			{
				query = query.OrderBy(orderQuery);
			}
			else
			{
				query = query.OrderBy(a => a.Id);
			}
		}
		else
		{
			if (filter.IsDescending) query = query.OrderByDescending(a => a.Id);
			else
			{
				query = query.OrderBy(a => a.Id);
			}
		}
		if (filter.PageSize == -1)
		{
			return new PagedListDto<Feedback>()
			{
				TotalCount = await query.CountAsync(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Data = await query.ToListAsync()
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageFeedbacks = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListDto<Feedback>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Data = pageFeedbacks
		};
	}

	public async Task<Feedback?> GetByIdAsync(int id)
	{
		return await appDbContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<PagedListDto<Feedback>?> GetListAsync(FilterDto filter)
	{
		IQueryable<Feedback> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Feedbacks;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Feedbacks.Where(x => x.Comment.ToLower().Contains(filter.Keyword) ||
										x.Rating.ToString().Contains(filter.Keyword) || x.Product!.Code == filter.Keyword);
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Feedback).GetProperties();
			var property = propertyInfo.FirstOrDefault(x => x.Name.Equals(filter.SortColumn, StringComparison.OrdinalIgnoreCase));
			var orderBy = filter.IsDescending ? "descending" : "ascending";
			if (property != null)
			{
				orderQueryBuilder.Append($"{property.Name.ToString()} {orderBy}");
			}
			string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
			if (!string.IsNullOrWhiteSpace(orderQuery))
			{
				query = query.OrderBy(orderQuery);
			}
			else
			{
				query = query.OrderBy(a => a.Id);
			}
		}
		else
		{
			if (filter.IsDescending) query = query.OrderByDescending(a => a.Id);
			else
			{
				query = query.OrderBy(a => a.Id);
			}
		}
		if (filter.PageSize == -1)
		{
			var data = await query.ToListAsync();
			return new PagedListDto<Feedback>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Data = data!
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageFeedbacks = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListDto<Feedback>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Data = pageFeedbacks
		};
	}

	public async Task<Feedback?> InsertAsync(Feedback feedback)
	{
		var orderDetails = await appDbContext.OrderDetails
					.Where(x=>x.Product!.Id == feedback.Product!.Id && x.User!.Id ==feedback.User!.Id)
					.ToListAsync();
		int countFeedbackAllow = 0;
		foreach (var orderDetail in orderDetails)
		{
			var order = await appDbContext.Orders
				.FirstOrDefaultAsync(x => x.Id == orderDetail.Order!.Id && x.Status == "Receive");
			if (order == null) continue;
			countFeedbackAllow++;
		}
		var countFeedbackCurrent = await appDbContext.OrderDetails
					.Where(x => x.Product!.Id == feedback.Product!.Id && x.User!.Id == feedback.User!.Id)
					.CountAsync();
		if (countFeedbackCurrent < countFeedbackAllow)
		{
			appDbContext.Feedbacks.Add(feedback);
			var result = await appDbContext.SaveChangesAsync();
			return result > 0 ? feedback : null;
		}
		else throw new BadHttpRequestException("Your current feedback limit has been reached. Please buy more products so you can give feedback");
	}

	public async Task<Feedback?> UpdateAsync(int id, Feedback feedback)
	{
		var feedbackModel = await appDbContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
		if (feedbackModel == null) throw new BadHttpRequestException("Feedback does not exist");
		if (feedbackModel.User!.Id != feedback.User!.Id) throw new UnauthorizedAccessException("Forbidden");
		feedbackModel.Comment = feedback.Comment;
		feedbackModel.Rating = feedback.Rating;
		feedbackModel.UpdatedDate = DateTime.Now;
		appDbContext.Entry(feedbackModel).State = EntityState.Modified;
		var result = await appDbContext.SaveChangesAsync();
		return result > 0 ? feedbackModel : null;
	}
}
