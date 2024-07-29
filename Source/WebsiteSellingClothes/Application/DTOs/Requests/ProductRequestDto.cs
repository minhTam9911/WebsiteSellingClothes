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

	[Required(ErrorMessage = "The short description is required")]
	[MaxLength(500, ErrorMessage = "The short description must be a maximum of 500 characters in length")]
	public string ShortDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "The long description is required")]
    [MaxLength(20000, ErrorMessage = "The long description must be a maximum of 20000 characters in length")]
    public string LongDescription { get; set; } = string.Empty;

    public bool IsActive { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "The brand must be between 1 and infinity")]
	public int BrandId { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "The category must be between 1 and infinity")]
	public int CategoryId { get; set; }

	//[Required(ErrorMessage ="The list image is required")]
	public IFormFile[]? Images { get; set; }
}
