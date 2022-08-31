using AutoMapper;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Profiles
{
    public class WorkoutPlanProfile : Profile
    {
        public WorkoutPlanProfile()
        {
            CreateMap<WorkoutPlanPutPostDto, WorkoutPlan>();
            CreateMap<WorkoutPlan, WorkoutPlanGetDto>();
            CreateMap<Routine, RoutineGetDto>();
            CreateMap<WorkoutSet, WorkoutSetGetDto>();
            CreateMap<Set, SetGetDto>();
            CreateMap<RoutinePostDto, Routine>();
            CreateMap<WorkoutSetPostDto, WorkoutSet>();
            CreateMap<SetPostDto, Set>();
        }
    }
}
