
using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces;
public interface IProductImageRepository
{

	Task<ProductImage?> InsertAsync(ProductImage productImage);
	Task<int> DeleteAsync(int id);
	Task<ProductImage?> UpdateAsync(int id, ProductImage productImage);
	Task<List<ProductImage>?> GetAllAsync();
	Task<ProductImage?> GetByIdAsync(int id);
    Task<List<ProductImage>?> GetByProductIdAsync(int idProduct);
    Task<List<ProductImage>?> GetByProductCodeAsync(string codeProduct);
    Task<PagedListDto<ProductImage>?> GetListAsync(FilterDto filter);

}
