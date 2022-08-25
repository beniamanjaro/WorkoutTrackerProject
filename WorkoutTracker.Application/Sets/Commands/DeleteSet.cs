using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Sets.Commands
{
    public class DeleteSet : IRequest<Set>
    {
        public int Id { get; set; }

    }
}
