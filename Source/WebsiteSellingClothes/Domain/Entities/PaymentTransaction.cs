using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class PaymentTransaction
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string TransactionMessage { get; set; } = string.Empty;
    public string TransactionPayload { get; set; } = string.Empty;
    public string TransactionStatus { get; set; } = string.Empty;
    public decimal TransactionAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public virtual Payment? Payment { get; set; }
}
