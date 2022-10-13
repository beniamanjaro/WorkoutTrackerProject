using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetMuscleSplitForRoutinesByWorkoutPlan : IRequest<List<Dictionary<string, int>>>
    {
        public int Id { get; set; }
    }
}
