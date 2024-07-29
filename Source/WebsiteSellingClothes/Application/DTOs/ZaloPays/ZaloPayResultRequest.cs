using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ZaloPays;
public class ZaloPayResultRequest
{
    public int appid { get; set; } 
    public string apptransid { get; set; } = string.Empty;
    public int pmcid { get; set; }
    public string bankcode { get; set; } = string.Empty;
    public long amount { get; set; }
    public long discountamount { get; set; }
    public int status { get; set; }
    public string checksum { get; set; } = string.Empty;

    public bool IsValidSignature(string key)
    {
        var inputData = appid + "|" + apptransid + "|" + pmcid + "|" + bankcode + "|" + amount + "|" + discountamount + "|" + status;
        var checkSignature = HashHelper.HmacSHA256(key,inputData);
        return this.checksum.Equals(checkSignature);
    }
}
