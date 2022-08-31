using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercises, IEnumerable<Exercise>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Exercise>> Handle(GetAllExercises request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExercisesRepository.GetAllExercises();
            return result;
        }
    }
}
