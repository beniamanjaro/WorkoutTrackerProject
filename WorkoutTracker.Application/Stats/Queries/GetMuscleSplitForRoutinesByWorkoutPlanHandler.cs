using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetMuscleSplitForRoutinesByWorkoutPlanHandler : IRequestHandler<GetMuscleSplitForRoutinesByWorkoutPlan, List<Dictionary<string, int>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMuscleSplitForRoutinesByWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Dictionary<string, int>>> Handle(GetMuscleSplitForRoutinesByWorkoutPlan request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.Id);
            if (workoutPlan == null) return null;

            var statsHelper = new StatsHelper(workoutPlan.Routines.ToList());

            var muscleSplits = statsHelper.GetMuscleSplitForRoutines();
            return muscleSplits;
        }
    }
}
