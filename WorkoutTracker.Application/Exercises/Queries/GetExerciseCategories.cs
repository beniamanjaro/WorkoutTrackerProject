﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetExerciseCategories : IRequest<IEnumerable<string>>
    {
    }
}