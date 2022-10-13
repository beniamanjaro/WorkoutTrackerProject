using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetExercisesByNameHandler : IRequestHandler<GetExercisesByName, IEnumerable<Exercise>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExercisesByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Exercise>> Handle(GetExercisesByName request, CancellationToken cancellationToken)
        {
            var exercises = await _unitOfWork.ExercisesRepository.GetExercisesByName(request.Name, request.PaginationFilter);
            return exercises;
        }
    }
}
