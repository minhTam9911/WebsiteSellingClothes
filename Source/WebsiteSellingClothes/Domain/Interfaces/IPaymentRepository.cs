using Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IPaymentRepository
{
    Task<Payment?> InsertAsync(Payment payment,Order order, Guid userId);
    Task<Payment?> UpdateAsync(string id, Payment payment);
    Task<int> DeleteAsync(string id);
    Task<List<Payment>?> GetAllAsync();
    Task<PagedListDto<Payment?>> GetListAsync(FilterDto filterDto);
    Task<PagedListDto<Payment?>> GetAllForMeAsync(Guid userId, FilterDto filterDto);
    Task<Payment?> GetByIdAsync(string id);
    Task<int> SetPaidAsync(string id, decimal paidAmount);
}
