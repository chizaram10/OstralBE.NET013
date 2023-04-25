using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<AuthenticationDTO>> LoginWithEmailAndPassword(LoginDTO loginDTO);
        Task<Result<AuthenticationDTO>> RegisterStudent(RegisterDTO registerDTO);
        Task<Result<AuthenticationDTO>> RegisterTutor(RegisterDTO registerDTO);
    }
}
