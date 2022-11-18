using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetSimilarExercisesHandler : IRequestHandler<GetSimilarExercises, List<Exercise>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSimilarExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Exercise>> Handle(GetSimilarExercises request, CancellationToken cancellationToken)
        {
            var exercise = await _unitOfWork.ExercisesRepository.GetExerciseById(request.ExerciseId);
            var exercises = await _unitOfWork.ExercisesRepository.GetAllExercisesWithNoPagination();

            var similarExercises = exercises.Where(e => e.Category == exercise.Category &&
            e.Muscle == exercise.Muscle && e.Equipment == exercise.Equipment && e.Id != exercise.Id).ToList();

            var util = new utilis<Exercise>();
            var shuffledSimilarExercises = util.ShuffleArr(similarExercises).Take(6).ToList();

            return shuffledSimilarExercises;

        }
    }
}
