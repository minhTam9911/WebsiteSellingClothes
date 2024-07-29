using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ZaloPays;
public class ZaloPayLinkResponse
{
    public int return_code { get; set; } 
    public string return_message { get; set; } = string.Empty;
    public int sub_return_code { get; set; }
    public string sub_return_message { get; set; } = string.Empty;
    public string order_url { get; set; } = string.Empty;
    public string zp_trans_token { get; set; } = string.Empty;
    public string order_token { get; set; } = string.Empty;
    public string qr_code { get; set; } = string.Empty;

   
}
