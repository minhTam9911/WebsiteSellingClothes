using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Infrastructure.Repositories;
public class ProductImageRepository : IProductImageRepository
{
    private readonly AppDbContext appDbContext;
    private readonly IWebHostEnvironment webHostEnvironment;
    public ProductImageRepository(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        this.appDbContext = appDbContext;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var productImage = await appDbContext.ProductImages.FirstOrDefaultAsync(x=>x.Id == id);
        if (productImage == null) throw new BadHttpRequestException("Product image does not exist");
        var pathDelete = Path.Combine(webHostEnvironment.WebRootPath, "products", productImage.Path!);
        
        appDbContext.ProductImages.Remove(productImage);
        var result = await appDbContext.SaveChangesAsync();
        if(result>0) File.Delete(pathDelete);
        return result;
    }

    public async Task<List<ProductImage>?> GetAllAsync()
    {
        return await appDbContext.ProductImages.ToListAsync();
    }

    public async Task<ProductImage?> GetByIdAsync(int id)
    {
        return await appDbContext.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ProductImage>?> GetByProductIdAsync(int idProduct)
    {
        return await appDbContext.ProductImages.Where(x => x.Product!.Id == idProduct).ToListAsync();
    }
    public async Task<List<ProductImage>?> GetByProductCodeAsync(string codeProduct)
    {
        return await appDbContext.ProductImages.Where(x => x.Product!.Code == codeProduct).ToListAsync();
    }

    public async Task<PagedListDto<ProductImage>?> GetListAsync(FilterDto filter)
    {
        IQueryable<ProductImage> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.ProductImages;
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.ProductImages.Where(x => 
                                            x.Path!.ToString().Contains(filter.Keyword) ||
                                            x.Product!.Name.ToLower().Contains(filter.Keyword)
                                            );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(ProductImage).GetProperties();
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
            return new PagedListDto<ProductImage>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageProductImages = await query.Skip((filter.PageIndex - 1) * filter.PageSize)
                                             .Take(filter.PageSize).ToListAsync();
        return new PagedListDto<ProductImage>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageProductImages
        };
    }

    public async Task<ProductImage?> InsertAsync(ProductImage productImage)
    {
        productImage.CreatedDate = DateTime.Now;
        productImage.UpdatedDate = DateTime.Now;
        appDbContext.ProductImages.Add(productImage);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? productImage : null;
    }

    public async Task<ProductImage?> UpdateAsync(int id, ProductImage productImage)
    {
        var productImageModel = await appDbContext.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
        if (productImageModel == null) throw new BadHttpRequestException("Product image does not exist");
        productImageModel.Path = productImage.Path;
        productImageModel.Product = productImage.Product;
        productImageModel.UpdatedDate = DateTime.Now;
        appDbContext.Entry(productImageModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? productImageModel : null;
     }
}
