using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetExercisesByMuscleGroup : IRequest<Dictionary<string, int>>
    {
        public int UserId { get; set; }
    }
}
