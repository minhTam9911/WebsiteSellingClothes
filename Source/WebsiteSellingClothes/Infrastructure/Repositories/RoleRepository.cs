using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class RoleRepository : IRoleRepository
{
	public readonly AppDbContext appDbContext;

	public RoleRepository(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

	public async Task<int> DeleteAsync(int id)
	{
		var role = await appDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
		if(role == null) throw new BadHttpRequestException("Role does not exist");
		appDbContext.Roles.Remove(role);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Role>?> GetAllAsync()
	{
		return await appDbContext.Roles.ToListAsync();
	}

	public async Task<Role?> GetByIdAsync(int id)
	{
		return await appDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<PagedListResponseDto<Role>?> GetListAsync(FilterRequestDto filter)
	{
		IQueryable<Role> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Roles;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Roles.Where(x => x.Name.ToLower().Contains(filter.Keyword) ||
			x.Description.ToLower().Contains(filter.Keyword));
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Role).GetProperties();
			var property = propertyInfo.FirstOrDefault(x => x.Name.Equals(filter.SortColumn, StringComparison.OrdinalIgnoreCase));
			var orderBy = filter.IsDescending ? "descending" : "ascending";
			if (property != null)
			{
				orderQueryBuilder.Append($"{property.Name.ToString()} {orderBy}");
			}
			string orderQuery = orderQueryBuilder.ToString().TrimEnd(',',' ');
			if(!string.IsNullOrWhiteSpace(orderQuery))
			{
				query = query.OrderBy(orderQuery);
			}
			else
			{
				query = query.OrderBy(a=>a.Id);
			}
		}
		else
		{
			if(filter.IsDescending)query = query.OrderByDescending(a=>a.Id);
			else
			{
				query = query.OrderBy(a=>a.Id);
			}
		}
		if (filter.PageSize == -1)
		{
			var data = await GetAllAsync();
			return new PagedListResponseDto<Role>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Items = data
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageRoles = await query.Skip((filter.PageIndex-1)*filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListResponseDto<Role>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Items = pageRoles
		};
	}

	public async Task<Role?> InsertAsync(Role role)
	{
		if (await appDbContext.Roles.FirstOrDefaultAsync(x => x.Name.ToLower() == role.Name.ToLower()) != null)
		{
			throw new BadHttpRequestException("Name already exists");
		}
		role.Name = role.Name.Trim();
		role.Description = role.Description.Trim();
		role.Created = DateTime.Now;
		role.Updated = DateTime.Now;
		await appDbContext.Roles.AddAsync(role);
		var result = await appDbContext.SaveChangesAsync()>0;
		return result ? role : null;
	}

	public async Task<Role?> UpdateAsync(int id, Role role)
	{
		var roleModel = await appDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
		if (roleModel == null) throw new BadHttpRequestException("Role doesn't exist");
		if(await appDbContext.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == role.Name && x.Id != roleModel.Id) != null)
		{
			throw new BadHttpRequestException("Name already exist");
		}
		roleModel.Name = role.Name.Trim();
		roleModel.Description = role.Description.Trim();
		roleModel.Updated = DateTime.Now;
		appDbContext.Entry(roleModel).State = EntityState.Modified;
		var result = await appDbContext.SaveChangesAsync();
		return result>0 ? roleModel : null;
	}
}
