using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserDTO>> GetUserByEmail(string email);

        Task<Result<UserDTO>> UpdateUserProfile(UserDTO updateUserDTO);
    }
}
