using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class RoleRequestDto
{
	[Required(ErrorMessage = "The name is required")]
	[MaxLength(50, ErrorMessage = "The name must be a maximum of 50 characters in length")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "The description is required")]
	[MaxLength(256, ErrorMessage = "The description must be a maximum of 256 characters in length")]
	public string Description { get; set; } = string.Empty;

}
