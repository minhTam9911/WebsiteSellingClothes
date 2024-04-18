using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses;
public record ApiResponseDto(
	string TokenType = null!,
	string AccessToken = null!,
	int Expires = 0,
	string RefreshToken = null!
);
