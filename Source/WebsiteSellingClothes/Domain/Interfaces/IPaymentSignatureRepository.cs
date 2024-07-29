using Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IPaymentSignatureRepository
{
    Task<PaymentSignature?> InsertAsync(PaymentSignature paymentSignature);
    Task<PaymentSignature?> UpdateAsync(string id, PaymentSignature paymentSignature);
    Task<int> DeleteAsync(string id);
    Task<List<PaymentSignature>?> GetAllAsync();
    Task<PagedListDto<PaymentSignature?>> GetListAsync(FilterDto filterDto);
    Task<PagedListDto<PaymentSignature?>> GetAllForMeAsync(Guid userId, FilterDto filterDto);
    Task<PaymentSignature?> GetByIdAsync(string id);
}
