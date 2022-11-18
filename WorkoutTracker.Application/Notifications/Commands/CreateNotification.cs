using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Notifications.Commands
{
    public class CreateNotification : IRequest<List<Notification>>
    {
        public int WorkoutPlanId { get; set; }
        public string Message { get; set; }
    }
}
