using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs;
public class FilterDto
{
	public string? Keyword { get; set; }
	public string? SortColumn { get; set; } 
	public bool IsDescending { get; set; }
	public int PageIndex { get; set; } = 1;
	public int PageSize { get; set; } = 2;
}
