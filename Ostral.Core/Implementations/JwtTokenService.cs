using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ostral.ConfigOptions;

namespace Ostral.Core.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly Jwt _jwt;

        public JwtTokenService(UserManager<User> userManager, Jwt jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }
        public async Task<string> GenerateAccessToken(User user)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.GivenName, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
            };
            var roles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Token));
            var token = new JwtSecurityToken
            (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.LifeTime),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature
             ));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) ?? throw new SecurityTokenException("Invalid token");
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = _jwt.Token != null,
                    ValidateLifetime = true,
                    ValidateAudience = _jwt.Audience != null,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwt.Issuer ?? null,
                    ValidAudience = _jwt.Audience ?? null,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Token ?? string.Empty))
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken
                    || jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException($"Error: {ex.Message}");
            }
        }

        public string ValidateAccessToken(string token)
        {
            var principal = GetPrincipalFromToken(token);
            if (principal.Identity is not ClaimsIdentity identity)
                return string.Empty;

            var userNameIdentifierClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            return userNameIdentifierClaim == null ? string.Empty : userNameIdentifierClaim.Value;
        }
    }
}