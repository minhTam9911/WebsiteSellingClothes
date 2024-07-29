using System.Security.Cryptography;
using System.Text;

namespace Common.Helplers;

public class RandomHelper
{
    public static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static string RandomInt(int length)
    {
        var random = new Random();
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static string RandomDefaultPassword(int length)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        var random = new Random();
        string password = new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
        return password;
    }

    public static string RandomTraceId()
    {
        // Generate a globally unique identifier (GUID)
        string guid = Guid.NewGuid().ToString("N");

        // Format the GUID according to the specified pattern
        string traceId = $"00-{guid.Substring(0, 16)}-{guid.Substring(16, 16)}-{guid.Substring(32)}";

        return traceId;
    }
   
}
