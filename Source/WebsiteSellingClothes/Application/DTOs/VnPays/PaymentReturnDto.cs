using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.VnPays;
public class PaymentReturnDto
{
    public string PaymentId { get; set; } = string.Empty;
    /// <summary>
    /// 00 : Success
    /// 99 : Unknown
    /// 10 : Error
    /// </summary>
    public string PaymentStatus { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string Signature { get; set;} = string.Empty;
    public decimal Amount { get; set; }
    public string PaymentDate { get; set;} = string.Empty;
    public string PaymentMessage { get; set; } = string.Empty;
    
}
