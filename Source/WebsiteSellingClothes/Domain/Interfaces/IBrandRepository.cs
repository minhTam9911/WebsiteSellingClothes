using Common.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;
public interface IBrandRepository
{
	Task<Brand?> InsertAsync(Brand brand,IFormFile image);
	Task<int> DeleteAsync(int id);
	Task<Brand?> UpdateAsync(int id, Brand brand, IFormFile? image);
	Task<List<Brand>?> GetAllAsync();
	Task<Brand?> GetByIdAsync(int id);
	Task<PagedListDto<Brand>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Brand>?> GetAllActiveAsync(bool isActive, int pageSize, int pageIndex);
}
