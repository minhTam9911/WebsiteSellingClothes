using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IRoleRepository 
{
	Task<Role?> InsertAsync(Role role);
	Task<int> DeleteAsync(int id);
	Task<Role?> UpdateAsync(int id, Role role);
	Task<List<Role>?> GetAllAsync();
	Task<Role?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Role>?> GetListAsync(FilterRequestDto filter);
}
