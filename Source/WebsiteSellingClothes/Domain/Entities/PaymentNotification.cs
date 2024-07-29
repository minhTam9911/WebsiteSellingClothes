using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class PaymentNotification
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public DateTime NotificaitonDate { get; set; }
    public string NotificationAmount { get; set; } = string.Empty;
    public string NotificationContent { get; set; } = string.Empty;
    public string NotificationMessage { get; set; } = string.Empty;
    public string NotificationSignature { get; set; } = string.Empty;
    public string NotificationStatus { get; set; } = string.Empty;
    public DateTime NotificationResDate { get; set; }
    public virtual Payment? Payment { get; set; }
    public virtual Merchant? Merchant { get; set; }
}
