using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class UpdateExercise : IRequest<Exercise>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Equipment { get; set; }

    }
}
