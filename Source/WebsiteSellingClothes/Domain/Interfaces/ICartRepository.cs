using Common.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface ICartRepository
{
	Task<Cart?> InsertAsync(int productId,int quantity, Guid userId);
	Task<int> DeleteAsync(int id, Guid userId);
	Task<Cart?> UpdateAsync(int id,int productId, int quantity, Guid userId);
	Task<Cart?> SetIsPaid(int id, Guid userId);
	Task<List<Cart>?> GetAllAsync();
	Task<Cart?> GetByIdAsync(int id,Guid userId);
	Task<PagedListDto<Cart>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Cart>?> GetAllForMeAsync(FilterDto filter, Guid userId);
	Task<int> SetPaidAsync(int id);
}
