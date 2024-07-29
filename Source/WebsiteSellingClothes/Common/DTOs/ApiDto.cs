using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs;
public record ApiDto(
	int Status, 
	bool Flag,
	string TokenType = null!,
	string AccessToken = null!,
	int Expires = 0,
	string RefreshToken = null!
);
