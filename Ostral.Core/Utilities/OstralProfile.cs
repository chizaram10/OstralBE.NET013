using AutoMapper;
using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Utilities
{
    public class OstralProfile : Profile
    {
        public OstralProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
        }    
        
    }
}