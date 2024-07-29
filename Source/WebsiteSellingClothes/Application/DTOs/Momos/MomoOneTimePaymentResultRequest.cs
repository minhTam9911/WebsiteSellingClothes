using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Momos;
public class MomoOneTimePaymentResultRequest
{
    public string deeplink { get; set; } = string.Empty;
    public string partnerCode { get; set; } = string.Empty;
    public long amount { get; set; }
    public string orderId { get; set; } = string.Empty;
    public string requestId { get; set; } = string.Empty;
    public string qrCodeUrl { get; set; } = string.Empty;
    public string deeplinkMiniApp { get; set; } = string.Empty;
    public string resultCode { get; set; } = string.Empty;
    public string payUrl { get; set; } = string.Empty;
   // public string message { get; set; } = string.Empty;
    public long responseTime { get; set; }
    public string signature { get; set; } = string.Empty;
    public bool IsValidSignature(string accessKey, string secretKey)
    {
        var rawSignature = "accessKey=" + accessKey +
           "&amount=" + this.amount +
           //"&extraData=" + this.extraData +
           //"&message=" + this.message +
           "&orderId=" + this.orderId +
           //"&orderInfo=" + this.orderInfo +
           // "&orderType=" + this.orderType +
           "&partnerCode=" + this.partnerCode +
           "&payUrl=" + this.payUrl +
           "&requestId=" + this.requestId +
           "&responseTime=" + this.responseTime +
           "&resultCode=" + this.resultCode;
           

        var checkSignature = HashHelper.HmacSHA256(secretKey, rawSignature);
        return this.signature.Equals(checkSignature);
    }
}
