using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetTopMostUsedWorkoutPlans : IRequest<List<string>>
    {
        public int UserId { get; set; }
        public int Size { get; set; }
    }
}
