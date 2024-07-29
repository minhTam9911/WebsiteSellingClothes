
using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces;
public interface IOrderDetailRepository
{
	Task<OrderDetail?> InsertAsync(OrderDetail orderDetail);
    Task<int?> DeleteAsync(int id, Guid userId);
    Task<int?> DeleteByOrderIdAsync(string orderId,Guid userId);
    Task<OrderDetail?> UpdateAsync(OrderDetail orderDetail);
    Task<List<OrderDetail>?> GetAllAsync();
    Task<OrderDetail?> GetByIdAsync(int id, Guid userId);
    Task<PagedListDto<OrderDetail>?> GetListAsync(FilterDto filter);
    Task<PagedListDto<OrderDetail>?> GetAllForMeAsync(FilterDto filter,Guid userId);
}
