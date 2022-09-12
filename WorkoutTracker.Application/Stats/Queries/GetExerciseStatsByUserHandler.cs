using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetExerciseStatsByUserHandler : IRequestHandler<GetExerciseStatsByUser, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExerciseStatsByUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(GetExerciseStatsByUser request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            if (completedRoutines.Count == 0) return 0;


            var statsHelper = new StatsHelper(completedRoutines);
            var maxWeight = statsHelper.GetMaxWeightByExercise(request.ExerciseName);


            return maxWeight;
        }
    }
}
