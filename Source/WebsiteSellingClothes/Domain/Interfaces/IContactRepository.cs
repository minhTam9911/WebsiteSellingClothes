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
public interface IContactRepository
{
	Task<Contact?> InsertAsync(Contact contact);
	Task<int> DeleteAsync(int id);
	Task<Contact?> UpdateAsync(int id, Contact contact);
	Task<List<Contact>?> GetAllAsync();
	Task<Contact?> GetByIdAsync(int id);
	Task<PagedListResponseDto<Contact>?> GetListAsync(FilterRequestDto filter);
}
