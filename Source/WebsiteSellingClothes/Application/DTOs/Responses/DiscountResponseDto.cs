using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class DiscountResponseDto
{
	public string Id { get; set; }
	public ICollection<ProductResponseDto>? Product { get; set; } = new List<ProductResponseDto>();
	public decimal Percentage { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
