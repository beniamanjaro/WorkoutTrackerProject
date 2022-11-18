using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Notifications.Commands
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotification, List<Notification>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNotificationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Notification>> Handle(CreateNotification request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.WorkoutPlanId);
            var usersToSendNotification = workoutPlan.Users.ToList();
            var notifications = new List<Notification>();

            foreach(var user in usersToSendNotification)
            {
                var notification = new Notification
                {
                    CreatedAt = DateTime.Now,
                    Message = request.Message,
                    UserId = user.Id,
                    WorkoutPlanId = request.WorkoutPlanId,
                    Seen = false,
                };
                notifications.Add(notification);

                user.Notifications.ToList()
                    .Add(notification);
            }

            await _unitOfWork.Save();

            return notifications;
        }
    }
}
