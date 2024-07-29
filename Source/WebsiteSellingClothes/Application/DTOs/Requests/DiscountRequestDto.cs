using Azure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class DiscountRequestDto
{
	[Required(ErrorMessage ="List product is required")]
	public List<int> ProductsId { get; set; } = new List<int>();

	[Required(ErrorMessage = "The percentage is required")]
	[Range(0.01,100.0,ErrorMessage = "The percentage must be between 0.01 and 100")]
	public decimal Percentage { get; set; }
	[Range(1, int.MaxValue, ErrorMessage = "The Quantity must be between 1 and infinity")]
	public int Quantity { get; set; }
	[Required(ErrorMessage ="The start date is required")]
	public DateTime StartDate { get; set; }

	[Required(ErrorMessage = "The end date is required")]
	public DateTime EndDate { get; set; }

}
