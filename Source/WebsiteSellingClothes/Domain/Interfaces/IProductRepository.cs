
using Domain.Entities;
using Common.DTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;
public interface IProductRepository
{
	Task<Product?> InsertAsync(Product product, IFormFile[]? images);
	Task<int> DeleteAsync(int id);
	Task<Product?> UpdateAsync(int id, Product product);
	Task<int> UploadImageAsync(int id, IFormFile[] images);
	Task<List<Product>?> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
    Task<List<Product>?> GetByCodeAsync(string code);
    Task<PagedListDto<Product>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Product>?> GetAllActiveAsync(bool isActive,FilterDto filter);
}
