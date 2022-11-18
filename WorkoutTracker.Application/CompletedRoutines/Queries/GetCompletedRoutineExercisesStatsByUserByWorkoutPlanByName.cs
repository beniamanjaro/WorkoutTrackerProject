using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Queries
{
    public class GetCompletedRoutineExercisesStatsByUserByWorkoutPlanByName : IRequest<List<CompletedRoutineExercise>>
    {
        public int UserId { get; set; }
        public int WorkoutPlanId { get; set; }
        public string RoutineName { get; set; }
    }
}
