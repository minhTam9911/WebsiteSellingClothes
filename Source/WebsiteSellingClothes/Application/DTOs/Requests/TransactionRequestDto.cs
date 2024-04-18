using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class TransactionRequestDto
{
	[Required(ErrorMessage = "The status is required")]
	[MaxLength(256, ErrorMessage = "The status must be a maximum of 256 characters in length")]
	public string Status { get; set; } = string.Empty;

	[Required(ErrorMessage = "The username is required")]
	[MinLength(8, ErrorMessage = "The username must be at least 8 characters long")]
	[RegularExpression(pattern: "^[a-zA-Z][a-zA-Z0-9]$", ErrorMessage = "Username only includes uppercase letters, lowercase letters and numbers")]
	[MaxLength(20, ErrorMessage = "The username must be a maximum of 20 characters in length")]
	public string Username { get; set; } = string.Empty;

	[EmailAddress(ErrorMessage = "Email invalidate")]
	[Required(ErrorMessage = "The email is required")]
	public string Email { get; set; } = string.Empty;

	[Phone(ErrorMessage = "Phone invalidate")]
	[Required(ErrorMessage = "The phone is required")]
	public string PhoneNumber { get; set; } = string.Empty;

	[Range(0.01,double.MaxValue,ErrorMessage = "The category must be between 0.01 and infinity")]
	public decimal Amount { get; set; }

	[Required(ErrorMessage = "The message is required")]
	[MaxLength(500, ErrorMessage = "The message must be a maximum of 500 characters in length")]
	public string Message { get; set; } = string.Empty;

	[Range(1, int.MaxValue, ErrorMessage = "The category must be between 1 and infinity")]
	public int PaymentId { get; set; }
}
