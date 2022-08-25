using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Users.Queries
{
    public class GetUserBydId : IRequest<User>
    {
        public int Id { get; set; }

    }
}
