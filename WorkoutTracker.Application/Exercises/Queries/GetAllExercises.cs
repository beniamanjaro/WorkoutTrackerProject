﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetAllExercises : IRequest<IEnumerable<Exercise>>
    {
    }
}