using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class PaymentDestinationResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string DestinationLogo { get; set; } = string.Empty;
    public string DestinationShortName { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string PaymentDestinationParentId { get; set; } = string.Empty;
}
