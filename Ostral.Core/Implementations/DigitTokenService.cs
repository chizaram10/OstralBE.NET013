using Microsoft.AspNetCore.Identity;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;
using System.Globalization;

namespace Ostral.Core.Implementations
{
    public class DigitTokenService : PhoneNumberTokenProvider<User>, IDigitTokenService
    {
        public const string DIGITPHONE = "DigitPhone";
        public const string DIGITEMAIL = "DigitEmail";

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        => Task.FromResult(false);

        public override async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
        {
            var token = new SecurityToken(await manager.CreateSecurityTokenAsync(user));
            var modifier = await GetUserModifierAsync(purpose, manager, user);
            var code = Rfc6238AuthenticationProvider.GenerateCode(token, modifier).ToString("D6", CultureInfo.InvariantCulture);
            return code;
        }

        public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
        {
            if (!Int32.TryParse(token, out int code))
                return false;

            var securityToken = new SecurityToken(await manager.CreateSecurityTokenAsync(user));
            var modifier = await GetUserModifierAsync(purpose, manager, user);
            var valid = Rfc6238AuthenticationProvider.ValidateCode(securityToken, code, modifier, token.Length);
            return valid;
        }

        public override Task<string> GetUserModifierAsync(string purpose, UserManager<User> manager, User user)
        {
            return base.GetUserModifierAsync(purpose, manager, user);
        }
    }
}