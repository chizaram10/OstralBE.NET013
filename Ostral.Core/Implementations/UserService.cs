using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _manager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<Result<UserDTO>> GetUserByEmail(string email)
        {
            var user = await _manager.FindByEmailAsync(email);
            if (user == null)
                return new Result<UserDTO>
                {
                    Errors = new List<string> { $"User not found for email {email}" }
                };

            var result = _mapper.Map<UserDTO>(user);
            return new Result<UserDTO>
            {
                Success = true,
                Data = result
            };
        }

        public async Task<Result<UserDTO>> UpdateUserProfile(UpdateUserDTO updateUserDTO)
        {
            var user = await _manager.FindByEmailAsync(updateUserDTO.Email);
            if (user == null)
                return new Result<UserDTO> { Errors = new List<string> { $"User not found for email {updateUserDTO.Email}" } };

            _mapper.Map(updateUserDTO, user);
            user.UpdatedAt = DateTime.UtcNow;

            await _manager.UpdateAsync(user);
            var result = _mapper.Map<UserDTO>(user);
            return new Result<UserDTO> { Data = result, Success = true };
        }
    }
}
