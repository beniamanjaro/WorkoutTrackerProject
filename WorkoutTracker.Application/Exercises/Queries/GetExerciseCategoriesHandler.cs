using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Exercises.Queries
{
    public class GetExerciseCategoriesHandler : IRequestHandler<GetExerciseCategories, IEnumerable<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExerciseCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<string>> Handle(GetExerciseCategories request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.ExercisesRepository.GetExercisesCategories();
            return categories;


        }
    }
}
