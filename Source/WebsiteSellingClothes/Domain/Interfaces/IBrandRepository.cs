using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
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
	Task<PagedListResponseDto<Brand>?> GetListAsync(FilterRequestDto filter);
	Task<Brand?> GetByStatus(bool isActive);
}
