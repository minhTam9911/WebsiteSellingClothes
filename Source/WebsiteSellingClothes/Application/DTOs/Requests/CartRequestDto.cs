using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class CartRequestDto
{
    [Range(1, int.MaxValue, ErrorMessage = "The product id must be between 1 and infinity")]
    public int ProductId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "The quantity must be between 1 and infinity")]
    public int Quantity { get; set; } = 1;

}
