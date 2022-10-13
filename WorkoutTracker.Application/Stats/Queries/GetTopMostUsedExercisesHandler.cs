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
    public class GetTopMostUsedExercisesHandler : IRequestHandler<GetTopMostUsedExercises, List<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopMostUsedExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<string>> Handle(GetTopMostUsedExercises request, CancellationToken cancellationToken)
        {
            var mostUsedExercises = new List<string>();
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var groupedExercises = completedRoutines.SelectMany(cr => cr.Exercises).AsEnumerable()
                    .GroupBy(s => s.Exercise.Name);
            var mostUsedFiveExercises = groupedExercises.OrderByDescending(e => e.Count()).Take(request.Size);
            foreach(var ex in mostUsedFiveExercises)
            {
                mostUsedExercises.Add(ex.Key);
            }

            return mostUsedExercises;
        }
    }
}
