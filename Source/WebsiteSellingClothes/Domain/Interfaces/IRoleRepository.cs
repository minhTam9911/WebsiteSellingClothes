
using Domain.Entities;
using Common.DTOs;
namespace Domain.Interfaces;
public interface IRoleRepository 
{
	Task<Role?> InsertAsync(Role role);
	Task<int> DeleteAsync(int id);
	Task<Role?> UpdateAsync(int id, Role role);
	Task<List<Role>?> GetAllAsync();
	Task<Role?> GetByIdAsync(int id);
	Task<PagedListDto<Role>?> GetListAsync(FilterDto filter);
}
