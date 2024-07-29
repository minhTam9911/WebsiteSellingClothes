
using Domain.Entities;
using Common.DTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;
public interface IUserRepository
{

	Task<User?> InsertAsync(User user);
	Task<int> DeleteAsync(Guid id);
	Task<User?> UpdateAsync(Guid id, User user);
	Task<int> UploadImageAsync(Guid id, IFormFile image);
    Task<int> DeleteImageAsync(Guid id);
    Task<List<User>?> GetAllAsync();
	Task<User?> GetByIdAsync(Guid id);
    Task<List<User>?> GetByRoleAsync(string name);
    Task<PagedListDto<User>?> GetListAsync(FilterDto filter);

}
