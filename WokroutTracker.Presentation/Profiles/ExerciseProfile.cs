using AutoMapper;
using System.Threading.Tasks.Dataflow;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseGetDto>();
            CreateMap<ExercisePutPostDto, Exercise>();
        }

    }
}
