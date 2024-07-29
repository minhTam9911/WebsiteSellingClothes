using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class ProductResponseDto
{
	public int Id { get; set; }
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public string Color { get; set; } = string.Empty;
	public string Size { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; }
	public int Purchase { get; set; }
	public int BrandId { get; set; }
	public string BrandName { get; set; } = string.Empty;
	public int CategoryId { get; set; }
	public string CategoryName { get; set; } = string.Empty;
	public ICollection<int> ProductImagesId { get; set; } = new List<int>();
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }

}
