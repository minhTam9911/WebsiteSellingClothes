using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IOrderRepository
{

	Task<Order?> InsertAsync(Order order);
	Task<int> DeleteAsync(int id);
	Task<Order?> UpdateAsync(int id, Order order);
	Task<List<Order>?> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Order>?> GetListAsync(FilterRequestDto filter);

}
