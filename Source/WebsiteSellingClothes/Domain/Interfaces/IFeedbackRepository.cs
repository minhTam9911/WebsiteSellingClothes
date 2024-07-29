
using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces;
public interface IFeedbackRepository
{
	Task<Feedback?> InsertAsync(Feedback feedback);
	Task<int> DeleteAsync(int id,Guid userId);
	Task<Feedback?> UpdateAsync(int id, Feedback feedback);
	Task<List<Feedback>?> GetAllAsync();
	Task<PagedListDto<Feedback>?> GetAllForProductAsync(string code, FilterDto filter);
	Task<Feedback?> GetByIdAsync(int id);
	Task<PagedListDto<Feedback>?> GetListAsync(FilterDto filter);
}
