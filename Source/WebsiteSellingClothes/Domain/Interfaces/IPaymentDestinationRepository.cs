using Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IPaymentDestinationRepository
{
    Task<PaymentDestination?> InsertAsync(PaymentDestination paymentDestination);
    Task<PaymentDestination?> UpdateAsync(string id, PaymentDestination paymentDestination);
    Task<int> DeleteAsync(string id);
    Task<List<PaymentDestination>?> GetAllAsync();
    Task<PagedListDto<PaymentDestination?>> GetListAsync(FilterDto filterDto);
    Task<PaymentDestination?> GetByIdAsync(string id);
}
