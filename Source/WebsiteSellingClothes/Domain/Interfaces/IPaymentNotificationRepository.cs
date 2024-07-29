using Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IPaymentNotificationRepository
{
    Task<PaymentNotification?> InsertAsync(PaymentNotification paymentNotification);
    Task<PaymentNotification?> UpdateAsync(string id, PaymentNotification paymentNotification);
    Task<int> DeleteAsync(string id);
    Task<List<PaymentNotification>?> GetAllAsync();
    Task<PagedListDto<PaymentNotification?>> GetListAsync(FilterDto filterDto);
    Task<PagedListDto<PaymentNotification?>> GetAllForMeAsync(Guid userId,FilterDto filterDto);
    Task<PaymentNotification?> GetByIdAsync(string id);
}
