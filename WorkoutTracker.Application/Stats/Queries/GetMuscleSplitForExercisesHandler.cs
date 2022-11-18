using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetMuscleSplitForExercisesHandler : IRequestHandler<GetMuscleSplitForExercises, List<KeyValuePair<string, int>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMuscleSplitForExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<KeyValuePair<string, int>>> Handle(GetMuscleSplitForExercises request, CancellationToken cancellationToken)
        {
            var completedRoutine = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutineById(request.CompletedRoutineId);
            var exercises = completedRoutine.Exercises.Select(e => e.Exercise).ToList();

            var statsHelper = new StatsHelper(exercises);

            var muscleSplit = statsHelper.GetMuscleSplitForExercises();

            return muscleSplit;
        }

    }
}
