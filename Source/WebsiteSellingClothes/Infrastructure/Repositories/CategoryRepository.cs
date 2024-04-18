using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class CategoryRepository : ICategoryRepository
{
	public readonly AppDbContext appDbContext;
	private readonly IWebHostEnvironment webHostEnvironment;
	public CategoryRepository(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
	{
		this.appDbContext = appDbContext;
		this.webHostEnvironment = webHostEnvironment;
	}

	public async Task<int> DeleteAsync(int id)
	{
		var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
		if (category == null) throw new BadHttpRequestException("Category does not exist");
		appDbContext.Categories.Remove(category);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Category>?> GetAllAsync()
	{
		return await appDbContext.Categories.ToListAsync();
	}

	public async Task<Category?> GetByIdAsync(int id)
	{
		return await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Category?> GetByStatus(bool isActive)
	{
		return await appDbContext.Categories.FirstOrDefaultAsync(x => x.IsActive == isActive);
	}

	public async Task<PagedListResponseDto<Category>?> GetListAsync(FilterRequestDto filter)
	{
		IQueryable<Category> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Categories;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Categories.Where(x => x.Name.ToLower().Contains(filter.Keyword) ||
			x.Description.ToLower().Contains(filter.Keyword));
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
			var data = await GetAllAsync();
			return new PagedListResponseDto<Category>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Items = data!
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageCategories = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListResponseDto<Category>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Items = pageCategories
		};
	}

	public async Task<Category?> InsertAsync(Category category, IFormFile image)
	{
		if (await appDbContext.Brands.FirstOrDefaultAsync(x => x.Name.ToLower() == category.Name.ToLower()) != null)
		{
			return null;
		}
		category.Name = category.Name.Trim();
		category.Description = category.Description.Trim();
		category.CreatedDate = DateTime.Now;
		category.UpdatedDate = DateTime.Now;
		if (!FileHelper.IsImage(image))
		{
			throw new IOException("Invalid file");
		}
		var fileName = FileHelper.GenerateFileName(image.FileName);
		var path = Path.Combine(webHostEnvironment.WebRootPath, "categories", fileName);
		using (var fileStream = new FileStream(path, FileMode.Create))
		{
			image.CopyTo(fileStream);
		}
		category.Image = fileName;
		await appDbContext.Categories.AddAsync(category);
		var result = await appDbContext.SaveChangesAsync() ;
		return result > 0 ? category : null;
	}

	public async Task<Category?> UpdateAsync(int id, Category category, IFormFile? image)
	{
		var categoryModel = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
		if (categoryModel == null) throw new BadHttpRequestException("Category does not exist");
		if (await appDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == category.Name && x.Id != categoryModel.Id) != null)
		{
			throw new BadHttpRequestException("Name already exist");
		}
		categoryModel.Name = category.Name.Trim();
		categoryModel.Description = category.Description.Trim();
		categoryModel.UpdatedDate = DateTime.Now;
		if (image != null)
		{
			if (!FileHelper.IsImage(image))
			{
				throw new IOException("Invalid file");
			}
			var pathDelete = Path.Combine(webHostEnvironment.WebRootPath, "categories", categoryModel.Image);
			File.Delete(pathDelete);
			var fileName = FileHelper.GenerateFileName(image.FileName);
			var path = Path.Combine(webHostEnvironment.WebRootPath, "categories", fileName);
			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				image.CopyTo(fileStream);
			}
			categoryModel.Image = fileName;
		}
		appDbContext.Entry(categoryModel).State = EntityState.Modified;
		var result = await appDbContext.SaveChangesAsync();
		return result > 0 ? categoryModel : null;
	}
}
