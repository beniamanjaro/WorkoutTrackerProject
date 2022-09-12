using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Queries
{
    public class GetCompletedRoutinesByUserByTimeframe : IRequest<List<CompletedRoutine>>
    {
        public int TimeframeInMonths { get; set; }
        public int UserId { get; set; }
    }
}
