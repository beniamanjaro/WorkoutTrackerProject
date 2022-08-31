using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Commands
{
    public class AddCompletedRoutine : IRequest<CompletedRoutine>
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public ICollection<WorkoutSet> WorkoutSets { get; set; }
    }
}
