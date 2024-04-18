using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IFeedbackRepository
{
	Task<Feedback?> InsertAsync(Feedback feedback);
	Task<int> DeleteAsync(int id);
	Task<Feedback?> UpdateAsync(int id, Feedback feedback);
	Task<List<Feedback>?> GetAllAsync();
	Task<Feedback?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Feedback>?> GetListAsync(FilterRequestDto filter);
}
