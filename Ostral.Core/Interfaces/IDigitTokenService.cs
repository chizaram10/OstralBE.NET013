using Microsoft.AspNetCore.Identity;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
    public interface IDigitTokenService
    {
        Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user);
        Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user);
    }
}
