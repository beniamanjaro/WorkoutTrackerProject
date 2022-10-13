using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Users.Commands
{
    public class UnsubscribeFromWorkoutPlan : IRequest<WorkoutPlan>
    {
        public int UserId { get; set; }
        public int WorkoutPlanId { get; set; }
    }
}
