using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetSimilarExercises : IRequest<List<Exercise>>
    {
        public int ExerciseId { get; set; }
    }
}
