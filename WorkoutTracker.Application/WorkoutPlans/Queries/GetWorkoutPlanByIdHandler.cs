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
    public class GetWorkoutPlanByIdHandler : IRequestHandler<GetWorkoutPlanById, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetWorkoutPlanByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlan> Handle(GetWorkoutPlanById request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.Id);
            return workoutPlan;
        }
    }
}
