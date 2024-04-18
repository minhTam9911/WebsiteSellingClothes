using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class ChangePasswordRequestDto
{
	[Required(ErrorMessage = "The current password is required")]
	public string? CurrentPassword { get; set; }

	[Required(ErrorMessage = "The new password is required")]
	[RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,20}$", ErrorMessage = "Validating minimum 8 characters and maximum of 20 characters, at-least 1 Uppercase Alphabet, 1 Lowercase Alphabet and 1 Number")]
	public string? NewPassword { get; set; }

	[Required(ErrorMessage = "The confirm password is required")]
	[Compare("NewPassword",ErrorMessage = "The confirm password not mathch new password")]
	public string? ConfirmPassword { get; set; }
}
