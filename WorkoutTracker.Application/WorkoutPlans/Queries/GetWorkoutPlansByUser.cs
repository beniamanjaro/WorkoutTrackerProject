using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Queries
{
    public class GetWorkoutPlansByUser : IRequest<IEnumerable<WorkoutPlan>>
    {
        public int UserId { get; set; }

    }
}
