using Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IMerchantRepository
{
    Task<Merchant?> InsertAsync(Merchant merchant, Guid userId);
    Task<Merchant?> UpdateAsync(string id, Merchant merchant);
    Task<int> DeleteAsync(string id);
    Task<List<Merchant>?> GetAllAsync();
    Task<PagedListDto<Merchant?>> GetListAsync(FilterDto filterDto);
    Task<PagedListDto<Merchant?>> GetAllForMeAsync(Guid userId,FilterDto filterDto);
    Task<Merchant?> GetByIdAsync(string id);
    Task<Merchant?> SetActiveAsync(string id);
}
