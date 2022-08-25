using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class UpdateExerciseHandler : IRequestHandler<UpdateExercise, Exercise>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExerciseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Exercise> Handle(UpdateExercise request, CancellationToken cancellationToken)
        {
            var exToUpdate = await _unitOfWork.ExercisesRepository.GetExerciseById(request.Id);
            if (exToUpdate == null) return null;

            if(request.Name != null) exToUpdate.Name = request.Name;
            if(request.Category != null) exToUpdate.Category = request.Category;
            if(request.Equipment != null) exToUpdate.Equipment = request.Equipment;

            await _unitOfWork.ExercisesRepository.UpdateExercise(exToUpdate);
            await _unitOfWork.Save();

            return exToUpdate;
        }
    }
}
