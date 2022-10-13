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
    public class GetMostUsedWorkoutPlansHandler : IRequestHandler<GetMostUsedWorkoutPlans, List<WorkoutPlan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMostUsedWorkoutPlansHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<WorkoutPlan>> Handle(GetMostUsedWorkoutPlans request, CancellationToken cancellationToken)
        {
            var mostUsedWorkoutPlans = await _unitOfWork.WorkoutPlansRepository.GetPopularWorkoutPlans(request.PaginationFilter);

            return mostUsedWorkoutPlans;

        }
    }
}
