using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IProductImageRepository
{

	Task<ProductImage?> InsertAsync(ProductImage productImage);
	Task<int> DeleteAsync(int id);
	Task<ProductImage?> UpdateAsync(int id, ProductImage productImage);
	Task<List<ProductImage>?> GetAllAsync();
	Task<ProductImage?> GetByIdAsync(int id);
	Task<PagedListResponseDto<ProductImage>?> GetListAsync(FilterRequestDto filter);

}
