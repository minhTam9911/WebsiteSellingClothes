
using Common.DTOs;
using Domain.Entities;

namespace Domain.Interfaces;
public interface IContactRepository
{
	Task<Contact?> InsertAsync(Contact contact);
	Task<int> DeleteAsync(int id);
	Task<List<Contact>?> GetAllAsync();
	Task<Contact?> GetByIdAsync(int id);
	Task<PagedListDto<Contact>?> GetListAsync(FilterDto filter);
}
