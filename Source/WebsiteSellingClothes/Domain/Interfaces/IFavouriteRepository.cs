
using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces;
public interface IFavouriteRepository
{
	Task<Favourite?> InsertAsync(int productId, Guid userId);
	Task<int> DeleteAsync(int id, Guid userId);
	Task<List<Favourite>?> GetAllAsync();
	Task<Favourite?> GetByIdAsync(int id, Guid userId);
	Task<PagedListDto<Favourite>?> GetListAsync(FilterDto filter);
	Task<PagedListDto<Favourite>?> GetAllForMeAsync(Guid userId, FilterDto filter);
}
