using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IFavouriteRepository
{
	Task<Favourite?> InsertAsync(Favourite discount);
	Task<int> DeleteAsync(int id);
	Task<Favourite?> UpdateAsync(int id, Favourite Category);
	Task<List<Favourite?>> GetAllAsync();
	Task<Favourite?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Favourite?>> GetListAsync(FilterRequestDto filter);
}
