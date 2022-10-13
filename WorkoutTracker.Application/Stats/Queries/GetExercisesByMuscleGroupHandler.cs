using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetExercisesByMuscleGroupHandler : IRequestHandler<GetExercisesByMuscleGroup, Dictionary<string, int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExercisesByMuscleGroupHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Dictionary<string, int>> Handle(GetExercisesByMuscleGroup request, CancellationToken cancellationToken)
        {
            var dict = new Dictionary<string, int>();

            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var groupedExercises = completedRoutines.SelectMany(cr => cr.Exercises).AsEnumerable()
                    .GroupBy(s => s.Exercise.Category);

            foreach(var group in groupedExercises)
            {
                var num = group.Count();
                if (dict.ContainsKey(group.Key))
                {
                    dict[group.Key] += num;
                } else
                {
                    dict[group.Key] = num;
                }

            }
            return dict;

        }
    }
}
