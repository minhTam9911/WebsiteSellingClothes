using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Momos;
public class MomoOneTimePaymentLinkResponse
{
    public string partnerCode { get; set; } = string.Empty;
    public long amount { get; set; }
    public string orderId { get; set; } = string.Empty;
    public string requestId { get; set; } = string.Empty;
    public long responseTime { get; set; }
    public string message { get; set; } = string.Empty;
    public string resultCode { get; set; } = string.Empty;
    public string payUrl { get; set; } = string.Empty;
    public string deepLink { get; set; } = string.Empty;
    public string qrCodeUrl { get; set; } = string.Empty;

}
