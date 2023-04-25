using Ostral.Domain.Models;
using System.Globalization;

namespace Ostral.Core.Utilities
{
    public static class EmailBodyBuilder
    {
        public static async Task<string> GetEmailBody(User user, string emailTempPath, string token)
        {
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;
            var userFirstName = textInfo.ToTitleCase(user.FirstName ?? "");
            var temp = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), emailTempPath));
            return temp.Replace("**code**", token).Replace("**user**", userFirstName);
        }
    }
}