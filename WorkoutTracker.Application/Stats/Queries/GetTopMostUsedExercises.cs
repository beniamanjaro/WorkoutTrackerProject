using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetTopMostUsedExercises : IRequest<List<TopExercise>>
    {
        public int Size { get; set; }
        public int UserId { get; set; }
    }
}
