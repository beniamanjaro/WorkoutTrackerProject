using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class DeleteExerciseHandler : IRequestHandler<DeleteExercise, Exercise>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Exercise> Handle(DeleteExercise request, CancellationToken cancellationToken)
        {
            var exToDelete = await _unitOfWork.ExercisesRepository.GetExerciseById(request.Id);
            if (exToDelete == null) return null;

            _unitOfWork.ExercisesRepository.DeleteExercise(exToDelete);
            await _unitOfWork.Save();

            return exToDelete;
        }
    }
}
