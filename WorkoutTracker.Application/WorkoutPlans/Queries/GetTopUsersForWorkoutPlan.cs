using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Queries
{
    public class GetTopUsersForWorkoutPlan : IRequest<List<TopUser>>
    {
        public int Id { get; set; }
    }
}
