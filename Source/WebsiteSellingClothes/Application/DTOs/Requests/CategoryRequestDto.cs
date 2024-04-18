using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class CategoryRequestDto
{
	[Required(ErrorMessage = "The name is required")]
	[MaxLength(256, ErrorMessage = "The name must be a maximum of 256 characters in length")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "The description is required")]
	[MinLength(10, ErrorMessage = "The description must be at least 10 characters long")]
	public string Description { get; set; } = string.Empty;

	[Ignore]
	public IFormFile? Image { get; set; }

	public bool IsActive { get; set; }
}
