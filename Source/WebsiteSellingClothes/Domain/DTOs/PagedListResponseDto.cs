using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses;
public class PagedListResponseDto<T>
{
	public List<T>? Items { get; set; }
	public int PageIndex { get; set; }
	public int PageSize { get; set; } 
	public int TotalCount { get; set; }
	public bool HasNextPage => PageIndex * PageSize < TotalCount;
	public bool HasPreviousPage => PageIndex >1;
}
