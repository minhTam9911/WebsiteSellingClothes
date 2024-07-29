using Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IPaymentTransactionRepository
{
    Task<PaymentTransaction?> InsertAsync(PaymentTransaction PaymentTransaction);
    Task<PaymentTransaction?> UpdateAsync(string id, PaymentTransaction paymentTransaction);
    Task<int> DeleteAsync(string id);
    Task<List<PaymentTransaction>?> GetAllAsync();
    Task<PagedListDto<PaymentTransaction?>> GetListAsync(FilterDto filterDto);
    Task<PagedListDto<PaymentTransaction?>> GetAllForMeAsync(Guid userId, FilterDto filterDto);
    Task<PaymentTransaction?> GetByIdAsync(string id);

}
