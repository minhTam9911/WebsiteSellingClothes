using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface ITransactionRepository
{

	Task<Transaction?> InsertAsync(Transaction transaction);
	Task<int> DeleteAsync(int id);
	Task<Transaction?> UpdateAsync(int id, Transaction transaction);
	Task<List<Transaction>?> GetAllAsync();
	Task<Transaction?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Transaction>?> GetListAsync(FilterRequestDto filter);

}
