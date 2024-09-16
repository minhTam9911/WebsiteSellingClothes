using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class RefreshTokenRequestDto
{
	
	[Required(ErrorMessage ="The refresh token is required")]
	public string RefreshToken { get; set; } = string.Empty;

	[Required(ErrorMessage = "The access token is required")]
	public string AccessToken { get; set; } = string.Empty;

}
