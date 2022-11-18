using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Commands
{
    public class AddCompletedRoutine : IRequest<CompletedRoutine>
    {
        public string RoutineName { get; set; }
        public string WorkoutPlanName { get; set; }
        public int? WorkoutPlanId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int TotalVolume { get; set; }
        public int TotalReps { get; set; }
        public int TotalSets { get; set; }
        public ICollection<CompletedRoutineExercise> Exercises { get; set; }
    }
}
