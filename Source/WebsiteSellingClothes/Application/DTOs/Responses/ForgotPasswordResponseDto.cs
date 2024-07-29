using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class ForgotPasswordResponseDto
{
	public int Status { get; set; }
	public bool  Flag { get; set; }
	public string Token { get; set; } = string.Empty;
}
