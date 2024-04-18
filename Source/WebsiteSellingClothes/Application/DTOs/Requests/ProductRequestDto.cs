using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class ProductRequestDto
{
	[Required(ErrorMessage = "The code is required")]
	[MaxLength(50, ErrorMessage = "The code must be a maximum of 50 characters in length")]
	public string Code { get; set; } = string.Empty;

	[Required(ErrorMessage = "The name is required")]
	[MaxLength(256, ErrorMessage = "The name must be a maximum of 256 characters in length")]
	public string Name { get; set; } = string.Empty;

	[Range(0.01, 1000000000.0,ErrorMessage = "The price must be between 0.01 and 1 billion")]
	public decimal Price { get; set; }

	[Required(ErrorMessage = "The color is required")]
	[MaxLength(50, ErrorMessage = "The color must be a maximum of 50 characters in length")]
	public string Color { get; set; } = string.Empty;

	[Required(ErrorMessage = "The size is required")]
	[MaxLength(50, ErrorMessage = "The size must be a maximum of 50 characters in length")]
	public string Size { get; set; } = string.Empty;

	[Range(1,int.MaxValue,ErrorMessage = "The price must be between 1 and infinity")]
	public int Quantity { get; set; }

	[Required(ErrorMessage = "The description is required")]
	[MaxLength(2000, ErrorMessage = "The description must be a maximum of 2000 characters in length")]
	public string Description { get; set; } = string.Empty;

	public bool IsActive { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "The brand must be between 1 and infinity")]
	public int BrandId { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "The category must be between 1 and infinity")]
	public int CategoryId { get; set; }

	//[Required(ErrorMessage ="The list image is required")]
	public IFormFile[]? Images { get; set; }
}
