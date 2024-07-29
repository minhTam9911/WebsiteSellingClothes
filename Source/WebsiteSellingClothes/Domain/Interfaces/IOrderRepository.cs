
using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces;
public interface IOrderRepository
{
	Task<Order?> InsertAsync(Order order);
    Task<int?> DeleteAsync(string id, Guid userId);
    Task<Order?> UpdateAsync(Order order);
    Task<List<Order>?> GetAllAsync();
	Task<Order?> GetByIdAsync(string id, Guid userId);
	Task<PagedListDto<Order>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Order>?> GetAllForMeAsync(Guid userId,FilterDto filter);
	Task<Order?> SetPaidAsync(string id, Payment payment);
}
