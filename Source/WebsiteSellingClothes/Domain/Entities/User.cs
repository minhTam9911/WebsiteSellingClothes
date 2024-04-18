using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class User
{
	[Key]
	public Guid Id { get; set; }
	public string FullName { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Gender { get; set; } = string.Empty;
	public DateTime DateOfBirth { get; set; }
	public string SecurityCode { get; set; } = string.Empty;
	public string RefreshToken { get; set; } = string.Empty;
	public DateTime RefeshTokenExpires { get; set; }
	public string TokenResetPassword { get; set; } = string.Empty;
	public DateTime TokenResetPasswordExpires { get; set; }
	public bool IsActive { get; set; }
	public virtual Role? Role { get; set; }
	public string Image { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
