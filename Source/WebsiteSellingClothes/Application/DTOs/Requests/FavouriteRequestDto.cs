using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class FavouriteRequestDto
{
	[Required(ErrorMessage ="The product is required")]
	public int? ProductId { get; set; }
}
