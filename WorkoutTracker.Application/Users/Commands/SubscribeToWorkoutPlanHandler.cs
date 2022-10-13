using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Users.Commands
{
    public class SubscribeToWorkoutPlanHandler : IRequestHandler<SubscribeToWorkoutPlan, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscribeToWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<WorkoutPlan> Handle(SubscribeToWorkoutPlan request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.GetUserById(request.UserId);
            var workoutPlanToSubscribeTo = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.WorkoutPlanId);

            if (user != null)
            {
                user.WorkoutPlans.Add((WorkoutPlan)workoutPlanToSubscribeTo);
            }
            await _unitOfWork.Save();

            return (WorkoutPlan)workoutPlanToSubscribeTo;

        }
    }
}
