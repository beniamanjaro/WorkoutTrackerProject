using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class DeleteExercise : IRequest<Exercise>
    {
        public int Id { get; set; }

    }
}
