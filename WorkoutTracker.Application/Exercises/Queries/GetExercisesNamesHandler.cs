using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetExercisesNamesHandler : IRequestHandler<GetExercisesNames, IList<ExerciseNameResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExercisesNamesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<ExerciseNameResponse>> Handle(GetExercisesNames request, CancellationToken cancellationToken)
        {
            var names = await _unitOfWork.ExercisesRepository.GetExercisesNames();
            return names;
        }
    }
}
