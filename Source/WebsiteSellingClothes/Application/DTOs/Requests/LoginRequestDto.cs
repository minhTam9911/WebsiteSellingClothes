using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class LoginRequestDto
{
	[Required(ErrorMessage = "The username is required")]
	public string UserName { get; set; } = string.Empty;
	[Required(ErrorMessage = "The password is required")]
	public string Password { get; set; } = string.Empty;

}
