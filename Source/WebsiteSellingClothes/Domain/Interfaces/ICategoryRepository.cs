using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Common.DTOs;

namespace Domain.Interfaces;
public interface ICategoryRepository
{
	Task<Category?> InsertAsync(Category category, IFormFile image);
	Task<int> DeleteAsync(int id);
	Task<Category?> UpdateAsync(int id, Category category, IFormFile? image);
	Task<List<Category>?> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
	Task<PagedListDto<Category>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Category>?> GetAllActiveAsync(bool isActive, int pageSize, int pageIndex);
}
