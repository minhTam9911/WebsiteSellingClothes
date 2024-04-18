using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Infrastructure.Repositories;
public class BrandRepository : IBrandRepository
{
	public readonly AppDbContext appDbContext;
	private readonly IWebHostEnvironment webHostEnvironment;

	public BrandRepository(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
	{
		this.appDbContext = appDbContext;
		this.webHostEnvironment = webHostEnvironment;
	}

	public async Task<int> DeleteAsync(int id)
	{
		var brand = await appDbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
		if (brand == null) throw new BadHttpRequestException("Brand does not exist");
		appDbContext.Brands.Remove(brand);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Brand>?> GetAllAsync()
	{
		return await appDbContext.Brands.ToListAsync();
	}

	public async Task<Brand?> GetByIdAsync(int id)
	{
		return await appDbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Brand?> GetByStatus(bool isActive)
	{
		return await appDbContext.Brands.FirstOrDefaultAsync(x => x.IsActive == isActive);
	}

	public async Task<PagedListResponseDto<Brand>?> GetListAsync(FilterRequestDto filter)
	{
		IQueryable<Brand> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Brands;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Brands.Where(x => x.Name.ToLower().Contains(filter.Keyword) ||
			x.Description.ToLower().Contains(filter.Keyword));
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Brand).GetProperties();
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
			return new PagedListResponseDto<Brand>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Items = data!
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageBrands = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListResponseDto<Brand>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Items = pageBrands
		};
	}

	public async Task<Brand?> InsertAsync(Brand brand, IFormFile image)
	{
		if (await appDbContext.Brands.FirstOrDefaultAsync(x => x.Name.ToLower() == brand.Name.ToLower()) != null)
		{
			throw new BadHttpRequestException("Name already exist");
		}
		brand.CreatedDate = DateTime.Now;
		brand.UpdatedDate = DateTime.Now;
		if (!FileHelper.IsImage(image))
		{
			throw new IOException("Invalid file");
		}
		var fileName = FileHelper.GenerateFileName(image.FileName);
		var path = Path.Combine(webHostEnvironment.WebRootPath, "brands", fileName);
		using (var fileStream = new FileStream(path, FileMode.Create))
		{
			image.CopyTo(fileStream);
		}
		brand.Image = fileName;
		await appDbContext.Brands.AddAsync(brand);
		var result = await appDbContext.SaveChangesAsync() > 0;
		return result ? brand : null;
	}

	public async Task<Brand?> UpdateAsync(int id, Brand brand, IFormFile? image)
	{
		var brandModel = await appDbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
		if (brandModel == null) throw new BadHttpRequestException("Brand does not exist");
		if (await appDbContext.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Name == brand.Name && x.Id != brandModel.Id) != null)
		{
			throw new BadHttpRequestException("Name already exist");
		}
		brandModel.Name = brand.Name;
		brandModel.Description = brand.Description;
		brandModel.UpdatedDate = DateTime.Now;
		if(image != null)
		{
			if (!FileHelper.IsImage(image))
			{
				throw new IOException("Invalid file");
			}
			var pathDelete = Path.Combine(webHostEnvironment.WebRootPath, "brands", brandModel.Image);
			File.Delete(pathDelete);
			var fileName = FileHelper.GenerateFileName(image.FileName);
			var path = Path.Combine(webHostEnvironment.WebRootPath, "brands", fileName);
			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				image.CopyTo(fileStream);
			}
			brandModel.Image = fileName;
		}
		appDbContext.Entry(brandModel).State = EntityState.Modified;
		var result = await appDbContext.SaveChangesAsync();
		return result > 0 ? brandModel : null;
	}
}
