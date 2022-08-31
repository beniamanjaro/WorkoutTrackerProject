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
    public class GetAllWorkoutPlansHandler : IRequestHandler<GetAllWorkoutPlans, IEnumerable<WorkoutPlan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllWorkoutPlansHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<WorkoutPlan>> Handle(GetAllWorkoutPlans request, CancellationToken cancellationToken)
        {
            var workoutPlans = await _unitOfWork.WorkoutPlansRepository.GetAllWorkoutPlans();
            return workoutPlans;

        }
    }
}
