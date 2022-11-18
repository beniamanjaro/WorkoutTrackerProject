using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetStatsByUser : IRequest<Dictionary<string, int>>
    {
        public int UserId { get; set; }
    }
}
