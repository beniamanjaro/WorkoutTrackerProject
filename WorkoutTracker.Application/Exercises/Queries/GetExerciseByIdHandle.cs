using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetExerciseByIdHandle : IRequestHandler<GetExerciseById, Exercise>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExerciseByIdHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Exercise> Handle(GetExerciseById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ExercisesRepository.GetExerciseById(request.Id);

        }
    }
}
