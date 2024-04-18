using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class VerifySecurityCodeRequestDto
{
	[Required(ErrorMessage = "The token is required")]
	public string? Token { get; set; }
	[Required(ErrorMessage = "The Code is required")]
	[RegularExpression(pattern: "^[0-9]+$",ErrorMessage ="The code numbers only")]
	[MaxLength(length:6,ErrorMessage = "The code only has 6 numbers")]
	[MinLength(length:6,ErrorMessage = "The code only has 6 numbers")]
	public string? Code { get; set; }
}
