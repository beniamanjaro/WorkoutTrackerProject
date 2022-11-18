using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetTopMostUsedWorkoutPlansHandler : IRequestHandler<GetTopMostUsedWorkoutPlans, List<TopWorkoutPlan>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopMostUsedWorkoutPlansHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TopWorkoutPlan>> Handle(GetTopMostUsedWorkoutPlans request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var mostUsedWorkoutPlans = completedRoutines.AsEnumerable()
                    .GroupBy(cr => cr.WorkoutPlanId, (key,group) => new TopWorkoutPlan()
                    {
                        Name = group.First().WorkoutPlanName,
                        Frequency = group.Count(),
                        Id = key,
                    })
                    .OrderByDescending(wp => wp.Frequency)
                    .Take(request.Size).ToList();

            return mostUsedWorkoutPlans;

        }
    }
}
