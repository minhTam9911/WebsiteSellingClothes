using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.VnPays;
public class VnPayResponseIpnDto
{
    public string ResponseCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
   
}
