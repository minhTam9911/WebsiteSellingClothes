using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class FeedbackRequestDto
{
	[Range(1,int.MaxValue,ErrorMessage = "The product must be between 1 and Infinity.")]
	public int ProductId { get; set; }

	[Range(1,5,ErrorMessage = "The rating must be between 1 and 5.")]
	public int Rating { get; set; }

	[Required(ErrorMessage = "The Comment is required")]
	[MaxLength(500, ErrorMessage = "The name must be a maximum of 500 characters in length")]
	public string Comment { get; set; } = string.Empty;

}
