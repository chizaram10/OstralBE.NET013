using AutoMapper;

namespace Ostral.Core.Utilities
{
	public class LoginProfile : Profile
	{
		public LoginProfile()
		{
			CreateMap<Domain.Models.User, DTOs.LoginDTO>();
		}
	}
}
