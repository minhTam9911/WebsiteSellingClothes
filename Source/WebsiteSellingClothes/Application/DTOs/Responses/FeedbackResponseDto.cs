using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class FeedbackResponseDto
{
	public int Id { get; set; }
	public int ProductId { get; set; }
	public Guid UserId { get; set; }
	public int Rating { get; set; }
	public string Comment { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
