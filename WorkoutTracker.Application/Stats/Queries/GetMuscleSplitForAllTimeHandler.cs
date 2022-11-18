using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetMuscleSplitForAllTimeHandler : IRequestHandler<GetMuscleSplitForAllTime, List<KeyValuePair<string, int>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMuscleSplitForAllTimeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<KeyValuePair<string, int>>> Handle(GetMuscleSplitForAllTime request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var exercises = completedRoutines.SelectMany(cr => cr.Exercises).Select(e => e.Exercise).ToList();

            var statsHelper = new StatsHelper(exercises);

            var muscleSplit = statsHelper.GetMuscleSplitForExercises();

            return muscleSplit;
        }
    }
}
