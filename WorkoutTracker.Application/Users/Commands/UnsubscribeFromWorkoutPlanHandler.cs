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
    public class UnsubscribeFromWorkoutPlanHandler : IRequestHandler<UnsubscribeFromWorkoutPlan, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnsubscribeFromWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlan> Handle(UnsubscribeFromWorkoutPlan request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.GetUserById(request.UserId);
            var workoutPlanToRemove = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.WorkoutPlanId);

            if (user == null) return null;
            if (workoutPlanToRemove == null) return null;
            
            if(user.WorkoutPlans.Any(wp => wp.Id == request.WorkoutPlanId))
            {
                user.WorkoutPlans.Remove(workoutPlanToRemove);
            }

            await _unitOfWork.Save();

            return workoutPlanToRemove;

        }
    }
}
