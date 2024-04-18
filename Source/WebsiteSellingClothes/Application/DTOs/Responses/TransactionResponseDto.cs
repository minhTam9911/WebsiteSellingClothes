using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class TransactionResponseDto
{
	public int Id { get; set; }
	public string Status { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public decimal Amount { get; set; }
	public string Message { get; set; } = string.Empty;
	public int PaymentId { get; set; }
	public string PaymentName { get; set; } = string.Empty;
	public UserResponseDto? User { get; set; }
	public DateTime CreatedDate { get; set; }

}
