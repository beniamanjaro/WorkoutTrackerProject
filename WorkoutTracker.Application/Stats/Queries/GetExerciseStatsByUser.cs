using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetExerciseStatsByUser : IRequest<double>
    {
        public int UserId { get; set; }
        public string ExerciseName { get; set; }
    }
}
