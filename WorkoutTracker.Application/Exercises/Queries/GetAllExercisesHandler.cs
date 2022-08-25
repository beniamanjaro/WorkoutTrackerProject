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
        private readonly IExercisesRepository _repository;

        public GetAllExercisesHandler(IExercisesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Exercise>> Handle(GetAllExercises request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllExercises();
            return result;
        }
    }
}
