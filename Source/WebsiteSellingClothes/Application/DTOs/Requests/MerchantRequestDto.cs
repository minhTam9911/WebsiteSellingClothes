using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class MerchantRequestDto
{
    public string MerchantName { get; set; } = string.Empty;
    public string MerchantWebLink { get; set; } = string.Empty;
    public string MerchantIpnUrl { get; set; } = string.Empty;
    public string MerchantReturnUrl { get; set; } = string.Empty;
}
