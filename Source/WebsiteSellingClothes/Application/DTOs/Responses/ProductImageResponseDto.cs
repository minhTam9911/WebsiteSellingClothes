using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class ProductImageResponseDto
{
	public int Id { get; set; }
	public string Path { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
