using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Commands
{
    public class CreateExercisesHandler : IRequestHandler<CreateExercises, List<Exercise>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Exercise>> Handle(CreateExercises request, CancellationToken cancellationToken)
        {

            var savedExercise = await _unitOfWork.ExercisesRepository.AddExercises(request.Exercises);
            await _unitOfWork.Save();
            return savedExercise;








        }
    }
}
