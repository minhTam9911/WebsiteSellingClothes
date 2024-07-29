using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class DiscountResponseDto
{
	public string Id { get; set; } = string.Empty;
	public ICollection<int>? ProductsId { get; set; } = new List<int>();
	public decimal Percentage { get; set; }
	public int Quantity { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
