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

namespace Infrastructure.Repositories;
public class FavouriteRepository : IFavouriteRepository
{
	private readonly AppDbContext appDbContext;

	public FavouriteRepository(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

	public async Task<int> DeleteAsync(int id, Guid userId)
	{
		var favourite = await appDbContext.Favourites.FirstOrDefaultAsync(x => x.Id == id);
		if (favourite == null) throw new BadHttpRequestException("Favourite does not exist");
		if(favourite.User!.Id != userId) throw new UnauthorizedAccessException("Forbidden");
		appDbContext.Favourites.Remove(favourite);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Favourite>?> GetAllAsync()
	{
		return await appDbContext.Favourites.ToListAsync();
	}

	private async Task<PagedListDto<Favourite>?> GetAllForMeAsync(Guid userId, int pageSize, int pageIndex)
	{
			var pageFavourites = await appDbContext.Favourites.Where(x => x.User!.Id == userId)
							.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
			var totalCount = appDbContext.Favourites.Where(x => x.User!.Id == userId).Count();
			var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
			return new PagedListDto<Favourite>()
			{
				TotalCount = totalCount,
				PageSize = pageSize,
				PageIndex = pageIndex,
				Data = pageFavourites
			};
	}

	public async Task<PagedListDto<Favourite>?> GetAllForMeAsync(Guid userId, FilterDto filter)
	{
		IQueryable<Favourite> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Favourites.Where(x => x.User!.Id == userId);
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Favourites.Where(x => x.Product!.Name.ToLower().Contains(filter.Keyword) &&
														x.User!.Id == userId);
		}	
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Category).GetProperties();
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
            return new PagedListDto<Favourite>()
            {
                TotalCount = data.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data
            };
            //return await GetAllForMeAsync(userId,filter.PageSize,filter.PageIndex);
        }
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageFavourites = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListDto<Favourite>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Data = pageFavourites
		};
	}

	public async Task<Favourite?> GetByIdAsync(int id, Guid userId)
	{
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user!.Role!.Id == 1) return await appDbContext.Favourites.FirstOrDefaultAsync(x => x.Id == id);
        var favourite = await appDbContext.Favourites.FirstOrDefaultAsync(x => x.Id == id);
        if (favourite!.User!.Id == userId) return favourite;
		throw new UnauthorizedAccessException("Forbidden");
	}

	public async Task<PagedListDto<Favourite>?> GetListAsync(FilterDto filter)
	{
		IQueryable<Favourite> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Favourites;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Favourites.Where(x => x.Product!.Name.ToLower().Contains(filter.Keyword));
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Favourite).GetProperties();
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
			return new PagedListDto<Favourite>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Data = data!
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageFavourites = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListDto<Favourite>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Data = pageFavourites
		};
	}

	public async Task<Favourite?> InsertAsync(int productId, Guid userId)
	{
		var product = await appDbContext.Products.FirstOrDefaultAsync(x=> x.Id == productId);
		if(product == null) throw new BadHttpRequestException("Product does not exist");
		var favourite = await appDbContext.Favourites
						.FirstOrDefaultAsync(x => x.Product!.Id == productId && x.User!.Id == userId);
		if (favourite != null) return favourite;
		else
		{
			favourite!.Product = product;
			favourite.User = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
			favourite.CreatedDate = DateTime.Now;
			favourite.UpdatedDate = DateTime.Now;
			appDbContext.Favourites.Add(favourite);
			var result = await appDbContext.SaveChangesAsync();
			return result > 0 ? favourite: null;
		}
	}
}
