using Ostral.Domain.Models;
using System.Security.Claims;

namespace Ostral.Core.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromToken(string token);
        string ValidateAccessToken(string token);
    }
}
