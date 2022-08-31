﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Routines.Commands
{
    public class DeleteRoutine : IRequest<Routine>
    {
        public int Id { get; set; }
    }
}
