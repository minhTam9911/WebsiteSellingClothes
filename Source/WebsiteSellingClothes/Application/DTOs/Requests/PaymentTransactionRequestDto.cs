using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class PaymentTransactionRequestDto
{
    public string TransactionMessage { get; set; } = string.Empty;
    public string TransactionPayload { get; set; } = string.Empty;
    public string TransactionStatus { get; set; } = string.Empty;
    public decimal TransactionAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string PaymentId { get; set; } = string.Empty;
}
