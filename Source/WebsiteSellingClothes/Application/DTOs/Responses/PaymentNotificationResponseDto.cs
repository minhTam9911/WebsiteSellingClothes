using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class PaymentNotificationResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string PaymentRefId { get; set; } = string.Empty;
    public DateTime NotificaitonDate { get; set; }
    public string NotificationAmount { get; set; } = string.Empty;
    public string NotificationContent { get; set; } = string.Empty;
    public string NotificationMessage { get; set; } = string.Empty;
    public string NotificationSignature { get; set; } = string.Empty;
    public string NotificationStatus { get; set; } = string.Empty;
    public DateTime NotificationResDate { get; set; }
    public string PaymentId { get; set; } = string.Empty;
    public string MerchantId { get; set; } = string.Empty;
}
