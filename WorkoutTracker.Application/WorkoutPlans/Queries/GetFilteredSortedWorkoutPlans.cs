using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Queries
{
    public class GetFilteredSortedWorkoutPlans : IRequest<PagedList<WorkoutPlan>>
    {
        public string SearchValue { get; set; }
        public PaginationFilter PaginationFilter { get; set; }
        public FilterSortData FilterSortData { get; set; }
    }
}
