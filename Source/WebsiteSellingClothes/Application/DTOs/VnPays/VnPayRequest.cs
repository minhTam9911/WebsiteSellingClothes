using Common.Helpers;
using System.Net;
using System.Text;

namespace Application.DTOs.VnPays;
public class VnPayRequest
{
    public SortedList<string, string> requestData = new SortedList<string, string>(new VnPayCompare());

    public VnPayRequest()
    {
    }

    public VnPayRequest(string version, string tmnCode, DateTime createDate, string currCode, decimal amount, string ipAddress, string orderType,string locale, string orderInfo, string returnUrl, string txnRef)
    {
        this.vnp_Amount = (int)amount * 100;
        this.vnp_Command = "pay";
        this.vnp_CreateDate = createDate.ToString("yyyyMMddHHmmss");
        this.vnp_CurrCode = currCode;
        this.vnp_Version = version;
        this.vnp_TmnCode = tmnCode;
        this.vnp_IpAddr = ipAddress;
        this.vnp_Locale = locale;
        this.vnp_OrderType = orderType;
        this.vnp_ReturnUrl = returnUrl;
        this.vnp_ExpireDate = createDate.AddMinutes(10).ToString("yyyyMMddHHmmss");
        this.vnp_TxnRef = txnRef;
        this.vnp_OrderInfo = orderInfo ;
    }

    public decimal? vnp_Amount { get; set; }
    public string vnp_Command { get; set; } = string.Empty;
    public string vnp_CreateDate { get; set; } = string.Empty;
    public string vnp_CurrCode { get; set; } = string.Empty;
    public string vnp_Version { get; set; } = string.Empty;
    public string vnp_TmnCode { get; set; } = string.Empty;
    public string vnp_BankCode { get; set; } = string.Empty;
    public string vnp_IpAddr { get; set; } = string.Empty;
    public string vnp_Locale { get; set; } = string.Empty;
    public string vnp_OrderType { get; set; } = string.Empty;
    public string vnp_ReturnUrl { get; set; } = string.Empty;
    public string vnp_ExpireDate { get; set; } = string.Empty;
    public string vnp_TxnRef { get; set; } = string.Empty;
    public string vnp_SecureHash { get; set; } = string.Empty;
    public string vnp_OrderInfo { get; set; } = string.Empty;


    public string GetLink(string baseUrl, string secretKey)
    {
        MakeRequestData();
        var data = new StringBuilder();
        foreach (KeyValuePair<string, string> kvp in requestData)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                data.Append(WebUtility.UrlEncode(kvp.Key) + "=" + WebUtility.UrlEncode(kvp.Value) + "&");
            }
        }
        string result = baseUrl + "?" + data.ToString();
        var secureHash = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
        return result + "vnp_SecureHash=" + secureHash;
    }


    public void MakeRequestData()
    {
        if (vnp_Amount.HasValue) requestData.Add("vnp_Amount", vnp_Amount.ToString() ?? string.Empty);
        if (!string.IsNullOrWhiteSpace(vnp_Command)) requestData.Add("vnp_Command", vnp_Command);
        if (!string.IsNullOrWhiteSpace(vnp_CreateDate)) requestData.Add("vnp_CreateDate", vnp_CreateDate);
        if (!string.IsNullOrWhiteSpace(vnp_CurrCode)) requestData.Add("vnp_CurrCode", vnp_CurrCode);
       
        if (!string.IsNullOrWhiteSpace(vnp_BankCode)) requestData.Add("vnp_BankCode", vnp_BankCode);
        if (!string.IsNullOrWhiteSpace(vnp_IpAddr)) requestData.Add("vnp_IpAddr", vnp_IpAddr);
        if (!string.IsNullOrWhiteSpace(vnp_Locale)) requestData.Add("vnp_Locale", vnp_Locale);
        if (!string.IsNullOrWhiteSpace(vnp_OrderInfo)) requestData.Add("vnp_OrderInfo", vnp_OrderInfo);
        if (!string.IsNullOrWhiteSpace(vnp_OrderType)) requestData.Add("vnp_OrderType", vnp_OrderType);
        if (!string.IsNullOrWhiteSpace(vnp_ReturnUrl)) requestData.Add("vnp_ReturnUrl", vnp_ReturnUrl);
        if (!string.IsNullOrWhiteSpace(vnp_TmnCode)) requestData.Add("vnp_TmnCode", vnp_TmnCode);
        if (!string.IsNullOrWhiteSpace(vnp_ExpireDate)) requestData.Add("vnp_ExpireDate", vnp_ExpireDate);
        if (!string.IsNullOrWhiteSpace(vnp_TxnRef)) requestData.Add("vnp_TxnRef", vnp_TxnRef);
        
        if (!string.IsNullOrWhiteSpace(vnp_Version)) requestData.Add("vnp_Version", vnp_Version);
    }

}

