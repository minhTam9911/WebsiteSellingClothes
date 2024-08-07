﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class ErrorDetailResponseDto
{
	public int Status { get; set; }
	public string Instanse { get; set; } = string.Empty;
	public string Error { get; set; } = string.Empty;
	public string Title { get; set;} = string.Empty;
	public string Type { get; set; } = string.Empty;
}
