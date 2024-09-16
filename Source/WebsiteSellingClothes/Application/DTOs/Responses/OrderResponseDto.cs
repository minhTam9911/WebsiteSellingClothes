using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class OrderResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string PaymentId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreateDate { get; set; }
    public ICollection<int?>? OrderDetailsId { get; set; } = new List<int?>();
    public string? DiscountId { get; set; } = string.Empty;
}
