using AutoMapper;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<User, UserGetIdDto>();
            CreateMap<UserPutPostDto, User>();
        }
    }
}
