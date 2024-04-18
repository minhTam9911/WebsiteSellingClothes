using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IOrderDetailRepository
{

	Task<OrderDetail?> InsertAsync(OrderDetail orderDetail);
	Task<int> DeleteAsync(int id);
	Task<OrderDetail?> UpdateAsync(int id, OrderDetail orderDetail);
	Task<List<OrderDetail>?> GetAllAsync();
	Task<OrderDetail?> GetByIdAsync(int id);
	Task<PagedListResponseDto<OrderDetail>?> GetListAsync(FilterRequestDto filter);

}
