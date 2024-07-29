using Common.DTOs;
using Common.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
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
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext appDbContext;
    private readonly IProductImageRepository productImageRepository; 
    private readonly IWebHostEnvironment webHostEnvironment;

    public ProductRepository(AppDbContext appDbContext, IProductImageRepository productImageRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.appDbContext = appDbContext;
        this.productImageRepository = productImageRepository;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) throw new BadHttpRequestException("Product does not exist");
        appDbContext.Products.Remove(product);
        var result = await appDbContext.SaveChangesAsync();
        await appDbContext.ProductImages.Where(x => x.Product!.Id == id).ExecuteDeleteAsync();
        await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<Product>?> GetAllAsync()
    {
        return await appDbContext.Products.ToListAsync();
    }

    public async Task<PagedListDto<Product>?> GetAllActiveAsync(bool isActive, FilterDto filter)
    {
        IQueryable<Product> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Products.Where(x=>x.IsActive == isActive);
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Products.Where(x => (x.Name.ToLower().Contains(filter.Keyword) ||
                                                x.ShortDescription.ToLower().Contains(filter.Keyword) ||
                                                x.LongDescription.ToLower().Contains(filter.Keyword) ||
                                                x.Color.ToLower().Contains(filter.Keyword) ||
                                                x.Size.ToLower().Contains(filter.Keyword) ||
                                                x.Price.ToString().Contains(filter.Keyword) ||
                                                x.Code.ToLower().Contains(filter.Keyword) ||
                                                x.Purchase.ToString().Contains(filter.Keyword) ||
                                                x.Brand!.Name.ToLower().Contains(filter.Keyword) ||
                                                x.Category!.Name.ToLower().Contains(filter.Keyword)) &&
                                                x.IsActive == isActive
                                                );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Product).GetProperties();
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
            return new PagedListDto<Product>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageProducts = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<Product>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageProducts
        };
    }

    public async Task<List<Product>?> GetByCodeAsync(string code)
    {
        return await appDbContext.Products.Where(x => x.Code == code).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedListDto<Product>?> GetListAsync(FilterDto filter)
    {
        IQueryable<Product> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Products;
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Products.Where(x => x.Name.ToLower().Contains(filter.Keyword) ||
                                                x.ShortDescription.ToLower().Contains(filter.Keyword) ||
                                                x.LongDescription.ToLower().Contains(filter.Keyword) ||
                                                x.Color.ToLower().Contains(filter.Keyword) ||
                                                x.Size.ToLower().Contains(filter.Keyword) ||
                                                x.Price.ToString().Contains(filter.Keyword) ||
                                                x.Code.ToLower().Contains(filter.Keyword) ||
                                                x.Purchase.ToString().Contains(filter.Keyword) ||
                                                x.Brand!.Name.ToLower().Contains(filter.Keyword) ||
                                                x.Category!.Name.ToLower().Contains(filter.Keyword)                      
                                                );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Product).GetProperties();
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
            return new PagedListDto<Product>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageProducts = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<Product>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageProducts
        };
    }

    public async Task<Product?> InsertAsync(Product product, IFormFile[]? images)
    {
        product.Purchase = 0;
        product.CreatedDate = DateTime.Now;
        product.UpdatedDate = DateTime.Now;
        appDbContext.Products.Add(product);
        var result = await appDbContext.SaveChangesAsync();
        if (result > 0)
        {
           if(images != null)
            {
                foreach (var item in images)
                {
                    var fileName = FileHelper.GenerateFileName(item.FileName);
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "products", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                    var productImage = new ProductImage()
                    {
                        Id = 0,
                        Path = fileName,
                        Product = product,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    var check = await productImageRepository.InsertAsync(productImage);
                }
            }
        }
        return result > 0 ? product : null;
    }

    public async Task<Product?> UpdateAsync(int id, Product product)
    {
        var productModel = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (productModel == null) throw new BadHttpRequestException("Product doesn't exist");
        productModel.Price = product.Price;
        productModel.Name = product.Name;
        productModel.Size = product.Size;
        productModel.Color = product.Color;
        productModel.Code = product.Code;
        productModel.Brand = product.Brand;
        productModel.Category = product.Category;
        productModel.UpdatedDate = DateTime.Now;
        productModel.ShortDescription = product.ShortDescription;
        productModel.LongDescription = product.LongDescription;
        productModel.Quantity = product.Quantity;
        productModel.IsActive = product.IsActive;
        appDbContext.Entry(productModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? productModel : null;
    }

    public async Task<int> UploadImageAsync(int id, IFormFile[] images)
    {
        var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) throw new BadHttpRequestException("Product does not exist");
        foreach(var item in images)
        {
                var fileName = FileHelper.GenerateFileName(item.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "products", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    item.CopyTo(fileStream);
                }
                var productImage = new ProductImage()
                {
                    Id = 0,
                    Path = fileName,
                    Product = product,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
          var check = await productImageRepository.InsertAsync(productImage);
        }
        return 1;
    }
}
