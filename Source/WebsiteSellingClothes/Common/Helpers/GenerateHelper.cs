using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers;
public class GenerateHelper
{
    public static string GenerateDiscountId()
    {
        Guid g = Guid.NewGuid();
        Random rn = new Random();
        string gs = g.ToString();
        int randomInt = rn.Next(10, 15 + 1);
        string output = gs.Substring(gs.Length - randomInt - 1, randomInt);
        return output.Replace("-","");
    }
    public static string GenerateMerchantId()
    {
        //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //int keySizeInBytes = 16;
        //byte[] key = new byte[keySizeInBytes];
        //rng.GetBytes(key);
        //string hexKey = BitConverter.ToString(key).Replace("-", "");
        return "MER" + DateTime.Now.ToString("yyMMddHHmmss");
    }

    public static string GenerateOrderId()
    {
        Random rnd = new Random();
       // long orderPart1 = rnd.Next(100000, 9999999);
        int orderPart = rnd.Next(1000, 9999999);
        return DateTime.Now.ToString("yyMMddHHmmss")+orderPart;
    }

    public static string GeneratePaymentId()
    {
        //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //int keySizeInBytes = 32;
        //byte[] key = new byte[keySizeInBytes];
        //rng.GetBytes(key);
        //string hexKey = BitConverter.ToString(key).Replace("-", "");
        //return "PAYMENT" + hexKey;
        var randomKey = DateTime.Now.Ticks.ToString();
        return randomKey;
    }

    public static string GenerateKeyNumber(string property)
    { 
        var randomKey = DateTime.Now.Ticks.ToString();
        var randomKeyToLong = long.Parse(randomKey) / 123456;
        return property + (long)randomKeyToLong;
    }

    public static string GenerateSecretKey()
    {
        byte[] secretKey = new byte[32];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(secretKey);
        }
        string secretKeyString = Convert.ToBase64String(secretKey);
        return secretKeyString;
    }
}
