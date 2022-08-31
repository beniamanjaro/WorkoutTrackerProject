using AutoMapper;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseGetDto>();
            CreateMap<ExercisePostDto, Exercise>();
            CreateMap<PrimaryMuscle, PrimaryMuscleGetDto>();
            CreateMap<SecondaryMuscle, SecondaryMuscleGetDto>();
        }

    }
}
