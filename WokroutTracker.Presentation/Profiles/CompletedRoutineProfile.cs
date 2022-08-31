using AutoMapper;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Profiles
{
    public class CompletedRoutineProfile : Profile
    {
        public CompletedRoutineProfile()
        {
            CreateMap<CompletedRoutine, CompletedRoutineGetDto>();
            CreateMap<CompletedRoutinePutPostDto, CompletedRoutine>();
        }
    }
}
