using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Enums;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<User> userManager,IStudentRepository studentRepository, IJwtTokenService jwtTokenService, IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _studentRepository = studentRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        
        public async Task<Result<AuthenticationDTO>> LoginWithEmailAndPassword(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                return new Result<AuthenticationDTO>
                {
                    Success = false,
                    Errors = new[] {"Invalid login credentials"}
                };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!userHasValidPassword)
                return new Result<AuthenticationDTO>
                {
                    Success = false,
                    Errors = new[] {"Invalid login credentials"}
                };

            return await GenerateAuthenticationDTO(user);
        }

        public async Task<Result<AuthenticationDTO>> RegisterStudent(RegisterDTO registerDTO)
        {
            var users = _mapper.Map<User>(registerDTO);
            var existingUser = await _userManager.FindByEmailAsync(users.Email!);
            if (existingUser != null)
                return new Result<AuthenticationDTO>
                {
                    Errors = new[] {"User with this email already exists"}
                };

            var user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                return new Result<AuthenticationDTO>
                {
                    Errors = new[] {$"Failed to create user: {string.Join(",", result.Errors)}"}
                };

            var student = new Student
            {
                User = user,
            };

            await _studentRepository.AddStudent(student);

            if (!await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Student.ToString()));
            if (await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
            }

            return await GenerateAuthenticationDTO(user);
        }

        public async Task<Result<AuthenticationDTO>> RegisterTutor(RegisterDTO registerDTO)
        {
            var users = _mapper.Map<User>(registerDTO);
            var existingUser = await _userManager.FindByEmailAsync(users.Email!);
            
            if (existingUser != null)
                return new Result<AuthenticationDTO>
                {
                    Errors = new[] {"User with this email already exists"}
                };

            var user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                return new Result<AuthenticationDTO>
                {
                    Errors = new[] {$"Failed to create user: {string.Join(",", result.Errors)}"}
                };

            if (!await _roleManager.RoleExistsAsync(UserRole.Tutor.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Tutor.ToString()));

            if (await _roleManager.RoleExistsAsync(UserRole.Tutor.ToString()))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Tutor.ToString());
            }

            return await GenerateAuthenticationDTO(user);
        }

        private async Task<Result<AuthenticationDTO>> GenerateAuthenticationDTO(User user)
        {
            var token = await _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMonths(6);

            AuthenticationDTO authenticationDTO = new AuthenticationDTO { RefreshToken = refreshToken, Token = token };
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new Result<AuthenticationDTO>
                {
                    Errors = new[] {$"Failed to update user's refresh token: {string.Join(",", result.Errors)}"}
                };

            return new Result<AuthenticationDTO>
            {
                Success = true,
                Data = authenticationDTO
            };
        }
    }
}