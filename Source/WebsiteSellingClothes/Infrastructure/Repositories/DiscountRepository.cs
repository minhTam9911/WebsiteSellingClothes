using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helplers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class DiscountRepository : IDiscountRepository
{
	public readonly AppDbContext appDbContext;

	public DiscountRepository(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

	public async Task<int> DeleteAsync(string id)
	{
		var discount = await appDbContext.Discounts.FirstOrDefaultAsync(x => x.Id == id);
		if (discount == null) throw new BadHttpRequestException("Role does not exist");
		appDbContext.Discounts.Remove(discount);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Discount>?> GetAllAsync()
	{
		return await appDbContext.Discounts.ToListAsync();
	}

	public async Task<Discount?> GetByIdAsync(string id)
	{
		return await appDbContext.Discounts.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<PagedListResponseDto<Discount>?> GetListAsync(FilterRequestDto filter)
	{
		IQueryable<Discount> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Discounts;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Discounts.Where(
				x => x.Id.ToLower().Contains(filter.Keyword) ||
				x.Percentage.ToString().Contains(filter.Keyword) || 
				x.StartDate.ToString().Contains(filter.Keyword) ||
				x.EndDate.ToString().Contains(filter.Keyword)
				);
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Discount).GetProperties();
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
			var data = await GetAllAsync();
			return new PagedListResponseDto<Discount>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Items = data
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageDiscounts = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListResponseDto<Discount>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Items = pageDiscounts
		};
	}

	public async Task<Discount?> InsertAsync(Discount discount)
	{
		discount.Id = RandomHelper.RandomDiscountKey();
		discount.CreatedDate = DateTime.Now;
		discount.UpdatedDate = DateTime.Now;
		await appDbContext.Discounts.AddAsync(discount);
		var result = await appDbContext.SaveChangesAsync() > 0;
		return result ? discount : null;
	}

	public async Task<Role?> UpdateAsync(string id, Discount discount)
	{
		var discountModel = await appDbContext.Discounts.FirstOrDefaultAsync(x => x.Id == id);
		if (discountModel == null) throw new BadHttpRequestException("Discount doesn't exist");
		discountModel.Percentage = discount.Percentage;
		discountModel.Quantity = discount.Quantity;
		discountModel.UpdatedDate = DateTime.Now;
		appDbContext.Entry(discountModel).State = EntityState.Modified;
		var result = await appDbContext.SaveChangesAsync();
		return result > 0 ? discountModel : null;
	}
}
