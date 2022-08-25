using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Commands
{
    public class DeleteWorkoutPlan : IRequest<WorkoutPlan>
    {
        public int Id { get; set; }

    }
}
