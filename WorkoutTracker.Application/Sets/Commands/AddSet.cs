using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Sets.Commands
{
    public class AddSet : IRequest<Set>
    {
        public int ExerciseId { get; set; }
        public int NumberOfReps { get; set; }
        public int Weight { get; set; }
        public int WorkoutSetId { get; set; }

    }
}
