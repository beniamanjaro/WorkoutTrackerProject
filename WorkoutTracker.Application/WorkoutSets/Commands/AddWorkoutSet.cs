using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutSets.Commands
{
    public class AddWorkoutSet : IRequest<WorkoutSet>
    {
        public int RoutineId { get; set; }
    }
}
