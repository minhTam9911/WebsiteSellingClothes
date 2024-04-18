using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class ForgotPasswordRequestDto
{
	[EmailAddress(ErrorMessage = "Email invalidate")]
	[Required(ErrorMessage = "The email is required")]
	public string Email { get; set; } = string.Empty;
}
