using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetMuscleSplitForExercises : IRequest<List<KeyValuePair<string, int>>>
    {
        public int CompletedRoutineId{ get; set; }
    }
}
