using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Commands
{
    public class CreateWorkoutPlan : IRequest<WorkoutPlan>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TimesPerWeek { get; set; }
        public ICollection<Routine> Routines { get; set; }
    }
}
