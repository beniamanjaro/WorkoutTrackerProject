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
    public class GetTopMostUsedExercisesHandler : IRequestHandler<GetTopMostUsedExercises, List<TopExercise>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopMostUsedExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TopExercise>> Handle(GetTopMostUsedExercises request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var mostUsedExercises = completedRoutines.SelectMany(cr => cr.Exercises).AsEnumerable()
                    .GroupBy(s => s.Exercise.Name,
                    (key, group) => new TopExercise()
                    { Name = key,
                      Frequency = group.Count(),
                      Id = group.First().ExerciseId
                    })
                    .OrderByDescending(e => e.Frequency)
                    .Take(request.Size).ToList();

            return mostUsedExercises;
        }
    }
}
