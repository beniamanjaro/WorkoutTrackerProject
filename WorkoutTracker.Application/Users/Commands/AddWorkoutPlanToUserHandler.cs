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
    public class AddWorkoutPlanToUserHandler : IRequestHandler<AddWorkoutPlanToUser, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddWorkoutPlanToUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlan> Handle(AddWorkoutPlanToUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.GetUserById(request.UserId);
            var workoutPlanToAdd = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.WorkoutPlanId);
            user.WorkoutPlans.Add(workoutPlanToAdd);

            await _unitOfWork.Save();

            return workoutPlanToAdd;
        }
    }
}
