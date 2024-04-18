using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class OrderRequestDto
{
	[Range(1, int.MaxValue, ErrorMessage = "The transaction must be between 1 and infinity")]
	public int TransactionId { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "The quantity must be between 1 and infinity")]
	public int Quantity { get; set; }

	[Range(0.01, double.MaxValue, ErrorMessage = "The category must be between 0.01 and infinity")]
	public decimal Amount { get; set; }

	[Required(ErrorMessage ="The status is required")]
	public string Status { get; set; } = string.Empty;
}
