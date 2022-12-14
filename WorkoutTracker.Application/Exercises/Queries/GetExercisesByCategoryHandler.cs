using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Queries
{
    internal class GetExercisesByCategoryHandler : IRequestHandler<GetExercisesByCategory, IEnumerable<Exercise>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExercisesByCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Exercise>> Handle(GetExercisesByCategory request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExercisesRepository.GetExercisesByCategory(request.Category, request.PaginationFilter);

            return result;
        }
    }
}
