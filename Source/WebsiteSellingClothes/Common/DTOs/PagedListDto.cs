using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs;
public class PagedListDto<T>
{
	public IReadOnlyList<T>? Data { get; set; }
	public int PageIndex { get; set; }
	public int PageSize { get; set; } 
	public int TotalCount { get; set; }
	public bool HasNextPage => PageIndex * PageSize < TotalCount;
	public bool HasPreviousPage => PageIndex >1;

    public static implicit operator PagedListDto<T>(PagedListDto<string> v)
    {
        throw new NotImplementedException();
    }
}
