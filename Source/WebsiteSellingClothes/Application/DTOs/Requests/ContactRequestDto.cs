using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class ContactRequestDto
{
	[Required(ErrorMessage = "The name is required")]
	[MinLength(5, ErrorMessage = "The name must be at least 5 characters long")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "The title is required")]
	[MinLength(5, ErrorMessage = "The title must be at least 5 characters long")]
	public string Title { get; set; } = string.Empty;

	[Required(ErrorMessage = "The content is required")]
	[MinLength(5, ErrorMessage = "The content must be at least 5 characters long")]
	[MaxLength(500, ErrorMessage = "The content must be a maximum of 500 characters in length")]
	public string Content { get; set; } = string.Empty;

	[EmailAddress(ErrorMessage = "Email invalidate")]
	[Required(ErrorMessage = "The email is required")]
	public string Email { get; set; } = string.Empty;

	[Phone(ErrorMessage = "Phone invalidate")]
	[Required(ErrorMessage ="The phone is required")]
	public string Phone { get; set; } = string.Empty;
}
