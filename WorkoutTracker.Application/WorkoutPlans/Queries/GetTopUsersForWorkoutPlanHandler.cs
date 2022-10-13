using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Queries
{
    public class GetTopUsersForWorkoutPlanHandler : IRequestHandler<GetTopUsersForWorkoutPlan, List<TopUser>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTopUsersForWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TopUser>> Handle(GetTopUsersForWorkoutPlan request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.Id);

            var users = await _unitOfWork.UsersRepository.GetAllUsers();
            var usersWithWorkoutPlans = users.Where(u => u.WorkoutPlans.Count() != 0); 
            var usersWithRequestedWorkoutPlan = usersWithWorkoutPlans.Where(u => u.WorkoutPlans.Any(wp => wp.Id == request.Id)).ToList();
            var topUsers = usersWithRequestedWorkoutPlan.Select(u => new TopUser { UserId = u.Id,Username = u.Username, Frequency = u.CompletedRoutines.Where(cr => cr.WorkoutPlanId == workoutPlan.Id).Count() }).ToList();

            return topUsers.OrderBy(u => u.Frequency).Take(5).ToList();
        }
    }
}
