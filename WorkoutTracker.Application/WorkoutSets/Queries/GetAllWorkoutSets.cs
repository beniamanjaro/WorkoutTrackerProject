using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutSets.Queries
{
    public class GetAllWorkoutSets : IRequest<IEnumerable<WorkoutSet>>
    {
    }
}
