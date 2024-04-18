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
public interface IUserRepository
{

	Task<User?> InsertAsync(User user);
	Task<int> DeleteAsync(int id);
	Task<User?> UpdateAsync(Guid id, User user,IFormFile? image);
	Task<List<User>?> GetAllAsync();
	Task<User?> GetByIdAsync(int id);
	Task<PagedListResponseDto<User>?> GetListAsync(FilterRequestDto filter);

}
