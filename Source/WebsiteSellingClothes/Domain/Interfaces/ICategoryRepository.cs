using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface ICategoryRepository
{
	Task<Category?> InsertAsync(Category category, IFormFile image);
	Task<int> DeleteAsync(int id);
	Task<Category?> UpdateAsync(int id, Category category, IFormFile? image);
	Task<List<Category>?> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Category>?> GetListAsync(FilterRequestDto filter);
}
