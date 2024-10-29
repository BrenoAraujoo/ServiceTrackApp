using System.Text;

namespace ServiceTrackHub.Domain.Common.Utils;

public class Utils
{

    public static string GenerateRandomString(int length = 32, bool uppercase = false)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var stringBuilder = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            var c = chars[random.Next(chars.Length)];
            stringBuilder.Append(c);
        }
        return uppercase ? stringBuilder.ToString().ToUpper()
            : stringBuilder.ToString();
    }
}