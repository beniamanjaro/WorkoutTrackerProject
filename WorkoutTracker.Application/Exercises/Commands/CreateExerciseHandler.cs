using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class CreateExerciseHandler : IRequestHandler<CreateExercise, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExerciseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateExercise request, CancellationToken cancellationToken)
        {
            var savedExercise = await _unitOfWork.ExercisesRepository.AddExercise(new Exercise() { Name = request.Name, Category = request.Category, Equipment = request.Equipment });
            await _unitOfWork.Save();
            return savedExercise.Id;

        }
    }
}
