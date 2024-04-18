using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IPaymentRepository
{
	Task<Payment?> InsertAsync(Payment payment);
	Task<int> DeleteAsync(int id);
	Task<Payment?> UpdateAsync(int id, Payment payment);
	Task<List<Payment>?> GetAllAsync();
	Task<Payment?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Payment>?> GetListAsync(FilterRequestDto filter);
}
