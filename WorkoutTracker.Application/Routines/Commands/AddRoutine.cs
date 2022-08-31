using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Routines.Commands
{
    public class AddRoutine : IRequest<Routine>
    {
        public int WorkoutPlanId { get; set; }
        public string Name { get; set; }
        public int DayOrderNumber { get; set; }
    }
}
