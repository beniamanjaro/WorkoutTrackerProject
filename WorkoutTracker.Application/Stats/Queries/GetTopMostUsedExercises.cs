using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetTopMostUsedExercises : IRequest<List<string>>
    {
        public int Size { get; set; }
        public int UserId { get; set; }
    }
}
