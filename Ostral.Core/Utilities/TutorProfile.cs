using AutoMapper;

namespace Ostral.Core.Utilities
{
	public class TutorProfile : Profile
	{
		public TutorProfile()
		{
			CreateMap<Domain.Models.Tutor, Core.DTOs.TutorDTO>().
				ForMember(
				dest => dest.FullName,
				opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
		}
	}
}
