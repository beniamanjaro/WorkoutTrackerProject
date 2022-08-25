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
    public class GetWorkoutPlansByUserHandler : IRequestHandler<GetWorkoutPlansByUser, IEnumerable<WorkoutPlan>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWorkoutPlansByUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<WorkoutPlan>> Handle(GetWorkoutPlansByUser request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlansByUser(request.UserId);
        }
    }
}
