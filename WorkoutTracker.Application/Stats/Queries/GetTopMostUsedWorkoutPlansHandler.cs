using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetTopMostUsedWorkoutPlansHandler : IRequestHandler<GetTopMostUsedWorkoutPlans, List<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopMostUsedWorkoutPlansHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<string>> Handle(GetTopMostUsedWorkoutPlans request, CancellationToken cancellationToken)
        {
            var result = new List<string>();
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var groupedExercises = completedRoutines.AsEnumerable()
                    .GroupBy(cr => cr.WorkoutPlanName);
            var mostUsedFiveWorkoutPlans = groupedExercises.OrderByDescending(wp => wp.Count()).Take(request.Size);
            foreach (var ex in mostUsedFiveWorkoutPlans)
            {
                result.Add(ex.Key);
            }

            return result;

        }
    }
}
