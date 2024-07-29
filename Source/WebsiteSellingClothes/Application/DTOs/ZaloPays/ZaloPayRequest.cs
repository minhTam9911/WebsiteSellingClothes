using Application.DTOs.Momos;
using Common.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ZaloPays;
public class ZaloPayRequest
{

    public ZaloPayRequest(int appId, string appUser, long appTime, long amount, string appTransId, string bankCode, string description, string callBackUrl, string redirectUrl)
    {
        this.app_id = appId;
        this.app_user = appUser;
        this.app_time = appTime;
        this.amount = amount;
        this.app_trans_id = DateTime.Now.ToString("yyMMdd") + "_" + appTransId;
        this.bank_code = string.Empty;
        this.description = description;
        this.callback_url = callBackUrl;
        this.redirecturl = redirectUrl;
    }

    public ZaloPayRequest()
    {
    }

    public int app_id { get; set; }
    public string app_user { get; set; } = string.Empty;
    public long app_time { get; set; }
    public long amount { get; set; }
    public string app_trans_id { get; set; } = string.Empty;
    public string bank_code { get; set; } = string.Empty;
    public string embed_data { get; set; } = string.Empty;
    public string item { get; set; } = "[]";
    public string callback_url { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string mac { get; set; } = string.Empty;
    public string redirecturl { get; set; } = string.Empty;


    public void MakeSignature(string key)
    {
        this.embed_data = JsonConvert.SerializeObject(new EmbedData() { redirecturl = this.redirecturl });
        var data = app_id + "|" + app_trans_id + "|" + app_user + "|" + amount + "|" + app_time + "|" + embed_data + "|" + item;
        this.mac = HashHelper.HmacSHA256(key, data);
    }

    //public Dictionary<string, string> GetContent()
    //{
    //    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
    //    keyValuePairs.Add("appid", AppId.ToString());
    //    //keyValuePairs.Add("app_user", AppUser);
    //    //keyValuePairs.Add("app_trans_id", AppTransId);
    //    keyValuePairs.Add("reqtime", AppTime.ToString());
    //    //keyValuePairs.Add("amount", Amount.ToString());
    //    //keyValuePairs.Add("item",JsonConvert.SerializeObject(Item));
    //    //  keyValuePairs.Add("description", Description);
    //    //keyValuePairs.Add("embed_data", JsonConvert.SerializeObject(EmbedData));
    //    //keyValuePairs.Add("bank_code", "zalopayapp");
    //    keyValuePairs.Add("mac", Mac);
    //    return keyValuePairs;
    //}

    public async Task<(bool, string)> GetLink(string paymentUrl)
    {
        using var httpClient = new HttpClient();
       
        var requestData = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
        {

            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,

        });
        var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");
        var createPaymentLinkResponse = await httpClient.PostAsync(paymentUrl, requestContent);
        if (createPaymentLinkResponse.IsSuccessStatusCode)
        {
            var responseContents = createPaymentLinkResponse.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<ZaloPayLinkResponse>(responseContents);

            if (responseData!.return_code == 1)
            {
                return (true, responseData.order_url);
            }
            else
            {
                return (false, responseData.return_message);
            }
        }
        else
        {
            return (false, createPaymentLinkResponse.ReasonPhrase)!;
        }
    }
}

public class EmbedData
{
    public List<string> preferred_payment_method { get; set; } = new List<string>();
    public string redirecturl { get; set; } = string.Empty;
}
