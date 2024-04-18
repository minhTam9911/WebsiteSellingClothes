using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IDiscountRepository
{
	Task<Discount?> InsertAsync(Discount discount);
	Task<int> DeleteAsync(string id);
	Task<Discount?> UpdateAsync(string id, Discount discount);
	Task<List<Discount>?> GetAllAsync();
	Task<Discount?> GetByIdAsync(string id);
	Task<PagedListResponseDto<Discount?>> GetListAsync(FilterRequestDto filter);
}
