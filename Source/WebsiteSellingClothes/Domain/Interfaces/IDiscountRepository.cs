
using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces;
public interface IDiscountRepository
{
	Task<Discount?> InsertAsync(Discount discount, int[] productsId);
	Task<int> DeleteAsync(string id);
	Task<Discount?> UpdateAsync(string id, Discount discount, int[]? productsId);
	Task<List<Discount>?> GetAllAsync();
	Task<Discount?> GetByIdAsync(string id);
	Task<PagedListDto<Discount>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Discount>?> GetAllActiveAsync(bool isActive, int pageSize, int pageIndex);
	Task<Discount?> UpdateQuantityAsync(string id, int quantity);
}
