using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class OrderDetailRequestDto
{
	[Range(1, int.MaxValue, ErrorMessage = "The order must be between 1 and infinity")]
	public int OrderId { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "The product must be between 1 and infinity")]
	public int ProductId { get; set; }

	[Required(ErrorMessage = "The address is required")]
	[MaxLength(256, ErrorMessage = "The address must be a maximum of 256 characters in length")]
	public string Address { get; set; } = string.Empty;

	[Range(1, int.MaxValue, ErrorMessage = "The quantity must be between 1 and infinity")]
	public int Quantity { get; set; }

	[Range(0.01, double.MaxValue, ErrorMessage = "The category must be between 0.01 and infinity")]
	public decimal Price { get; set; }

	[Range(0.01, int.MaxValue, ErrorMessage = "The category must be between 0.01 and infinity")]
	public decimal TotalAmount { get; set; }
}
