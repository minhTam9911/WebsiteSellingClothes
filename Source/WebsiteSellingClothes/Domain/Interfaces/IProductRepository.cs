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
public interface IProductRepository
{
	Task<Product?> InsertAsync(Product product, IFormFile[] image);
	Task<int> DeleteAsync(int id);
	Task<Product?> UpdateAsync(int id, Product product, IFormFile[]? image);
	Task<List<Product>?> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Product>?> GetListAsync(FilterRequestDto filter);

}
