using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class CartResponseDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Guid UserId { get; set; }
    public int Quantity { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

}
