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
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercises, PagedList<Exercise>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Exercise>> Handle(GetAllExercises request, CancellationToken cancellationToken)
        {
            var exercises = await _unitOfWork.ExercisesRepository.GetAllExercises(request.PaginationFilter);
            return exercises;
        }
    }
}
