using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Payment
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string PaymentContent { get; set; } = string.Empty;
    public string PaymentCurrency { get; set; } = string.Empty;
    public decimal RequiredAmount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public DateTime ExpireDate { get; set; }
    public string PaymentLanguage { get; set; } = string.Empty;
    public virtual Merchant? Merchant { get; set; }
    public virtual PaymentDestination? PaymentDestination { get; set; }
    public decimal PaidAmount { get; set; }
    /// <summary>
    /// 0 : Payment has been processed
    /// 1 : Payment has not been processed yet
    /// </summary>
    public string PaymentStatus { get; set; } = string.Empty;
    public string PaymentLastMessage { get; set; } = string.Empty;
    public virtual User? User { get; set; }
    [ForeignKey("Order")]
    public string? OrderId { get; set; } = string.Empty;
    public virtual Order? Order { get; set; }
}
