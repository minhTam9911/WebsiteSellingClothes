using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class PaymentRequestDto
{
    public string PaymentContent { get; set; } = string.Empty;
    public string PaymentCurrency { get; set; } = "VND";
    public string OrderId { get; set; } = string.Empty;
    public decimal RequiredAmount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
    public string PaymentLanguage { get; set; } = "vn";
    public string MerchantId { get; set; } = string.Empty;
    public string PaymentDestinationId { get; set; } = string.Empty;
   
  //  public string Signture { get; set; } = string.Empty;
}
