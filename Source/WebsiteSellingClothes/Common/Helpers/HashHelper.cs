using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Common.Helpers;
public class HashHelper
{
    public static string HmacSHA512(string key, string inputData)
    {
        var hash = new StringBuilder();
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            byte[] hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {

                hash.Append(theByte.ToString("x2"));

            }
        }
        return hash.ToString();
    }
    public static string HmacSHA256(string key, string inputData)
    {
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] inputDataBytes = encoding.GetBytes(inputData);
        byte[] keyBytes = encoding.GetBytes(key);
        byte[] hashBytes;
        using (HMACSHA256 hash = new HMACSHA256(keyBytes))
            hashBytes = hash.ComputeHash(inputDataBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
