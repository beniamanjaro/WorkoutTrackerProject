using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Queries
{
    public class GetCompletedRoutinesByUserByWorkoutPlan : IRequest<PagedList<CompletedRoutine>>
    {
        public int UserId { get; set; }
        public int WorkoutPlanId { get; set; }
        public PaginationFilter PaginationFilter { get; set; }
    }
}
