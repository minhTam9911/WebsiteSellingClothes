using System.Text;

namespace Infrastructure.Helplers;

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
	public static string RandomDiscountKey()
	{
		Guid g = Guid.NewGuid();
		Random rn = new Random();
		string gs = g.ToString();
		int randomInt = rn.Next(5, 10 + 1);
		string output = gs.Substring(gs.Length - randomInt - 1, randomInt);
		return output;
	}
}
