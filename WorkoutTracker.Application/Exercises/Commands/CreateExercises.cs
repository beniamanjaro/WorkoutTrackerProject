using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class CreateExercises : IRequest<List<Exercise>>
    {
        public List<Exercise> Exercises { get; set; }


    }
}
