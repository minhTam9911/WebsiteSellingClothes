using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class UserResponseDto
{
	public Guid Id { get; set; }
	public string FullName { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Gender { get; set; } = string.Empty;
	public DateTime DateOfBirth { get; set; } = DateTime.Now;
	public bool IsActive { get; set; }
	public int RoleId { get; set; }
	public string Image { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
