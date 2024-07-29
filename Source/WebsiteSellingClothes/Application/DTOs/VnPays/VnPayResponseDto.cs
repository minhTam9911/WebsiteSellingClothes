using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.VnPays;
public class VnPayResponseDto
{
    public SortedList<string, string> responseData = new SortedList<string, string>(new VnPayCompare());

    public int? vnp_Amount { get; set; }
    public string vnp_TmnCode { get; set; } = string.Empty;
    public string vnp_BankCode { get; set; } = string.Empty;
    public string vnp_BankTranNo { get; set; } = string.Empty;
    public string vnp_CardType { get; set; } = string.Empty;
    public string vnp_TransactionNo { get; set; } = string.Empty;
    public string vnp_TransactionStatus { get; set; } = string.Empty;
    public string vnp_TxnRef { get; set; } = string.Empty;
    public string vnp_SecureHash { get; set; } = string.Empty;
    public string vnp_OrderInfo { get; set; } = string.Empty;
    public string vnp_PayDate { get; set; } = string.Empty;
    public string vnp_ResponseCode { get; set; } = string.Empty;

    public string vnp_TransactionType { get; set; } = string.Empty;
    public string vnp_Command { get; set; } = string.Empty;
    public string vnp_ResponseId { get; set; } = string.Empty;
    public string vnp_Message { get; set; } = string.Empty;

    public bool IsValidSignature(string secretKey)
    {
        MakeResponseData();
        StringBuilder data = new StringBuilder();
        foreach (KeyValuePair<string, string> kvp in responseData)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                data.Append(WebUtility.UrlEncode(kvp.Key) + "=" + WebUtility.UrlEncode(kvp.Value) + "&");
            }
        }
        string checkSum  = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
        return checkSum.Equals(this.vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
    }

    public void MakeResponseData()
    {
        if (vnp_Amount != null) responseData.Add("vnp_Amount", vnp_Amount.ToString() ?? string.Empty);
        if (!string.IsNullOrWhiteSpace(vnp_TmnCode)) responseData.Add("vnp_TmnCode", vnp_TmnCode);
        if (!string.IsNullOrWhiteSpace(vnp_BankCode)) responseData.Add("vnp_BankCode", vnp_BankCode);
        if (!string.IsNullOrWhiteSpace(vnp_BankTranNo)) responseData.Add("vnp_BankTranNo", vnp_BankTranNo);
        if (!string.IsNullOrWhiteSpace(vnp_CardType)) responseData.Add("vnp_CardType", vnp_CardType);
        if (!string.IsNullOrWhiteSpace(vnp_OrderInfo)) responseData.Add("vnp_OrderInfo", vnp_OrderInfo);
        if (!string.IsNullOrWhiteSpace(vnp_TransactionNo)) responseData.Add("vnp_TransactionNo", vnp_TransactionNo);
        if (!string.IsNullOrWhiteSpace(vnp_TransactionStatus)) responseData.Add("vnp_TransactionStatus", vnp_TransactionStatus);
        
        if (!string.IsNullOrWhiteSpace(vnp_TxnRef)) responseData.Add("vnp_TxnRef", vnp_TxnRef);
        if (!string.IsNullOrWhiteSpace(vnp_PayDate)) responseData.Add("vnp_PayDate", vnp_PayDate);
        if (!string.IsNullOrWhiteSpace(vnp_ResponseCode)) responseData.Add("vnp_ResponseCode", vnp_ResponseCode);
    }
}
