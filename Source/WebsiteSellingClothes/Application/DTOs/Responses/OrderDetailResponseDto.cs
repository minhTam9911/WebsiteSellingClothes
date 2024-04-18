using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class OrderDetailResponseDto
{
	public int Id { get; set; }
	public virtual OrderResponseDto? Order { get; set; }
	public virtual ProductResponseDto? Product { get; set; }
	public string Address { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public decimal Price { get; set; }
	public decimal TotalAmount { get; set; }
}
