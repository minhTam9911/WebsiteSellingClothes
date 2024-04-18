using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class UserRequestDto
{
	[Required(ErrorMessage = "The name is required")]
	[MinLength(5, ErrorMessage = "The name must be at least 5 characters long")]
	public string FullName { get; set; } = string.Empty;

	[Required(ErrorMessage = "The address is required")]
	[MinLength(5, ErrorMessage = "The address must be at least 5 characters long")]
	public string Address { get; set; } = string.Empty;

	[Required(ErrorMessage = "The phone number is required")]
	[Phone(ErrorMessage = "Phone number invalidate")]
	public string PhoneNumber { get; set; } = string.Empty;

	[EmailAddress(ErrorMessage = "Email invalidate")]
	[Required(ErrorMessage = "The email is required")]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "The username is required")]
	[RegularExpression(pattern: "^[a-zA-Z][a-zA-Z0-9]{8,20}$", ErrorMessage = "Username only includes uppercase letters, lowercase letters and numbers,Validating minimum 8 characters and maximum of 20 characters.")]
	public string Username { get; set; } = string.Empty;

	[Required(ErrorMessage = "The password is required")]
	[RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,20}$",ErrorMessage = "Validating minimum 8 characters and maximum of 20 characters, at-least 1 Uppercase Alphabet, 1 Lowercase Alphabet and 1 Number")]
	public string Password { get; set; } = string.Empty;

	[Required(ErrorMessage = "The gender is required")]
	public string Gender { get; set; } = string.Empty;

	[Required(ErrorMessage ="Date of birth is required")] 
	public DateTime DateOfBirth { get; set; }

	//[Required(ErrorMessage ="The file is required")]
	public IFormFile? Image { get; set; } 
}
