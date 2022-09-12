using AutoMapper;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Profiles
{
    public class WorkoutPlanProfile : Profile
    {
        public WorkoutPlanProfile()
        {
            CreateMap<WorkoutPlanPostDto, WorkoutPlan>();
            CreateMap<WorkoutPlan, WorkoutPlanGetDto>();
            CreateMap<Routine, RoutineGetDto>();
            CreateMap<WorkoutSet, WorkoutSetGetDto>();
            CreateMap<Set, SetGetDto>();
            CreateMap<RoutinePutPostDto, Routine>();
            CreateMap<WorkoutSetPutPostDto, WorkoutSet>();
            CreateMap<SetPutPostDto, Set>();
        }
    }
}
