using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutSets.Queries
{
    public class GetAllWorkoutSetsHandler : IRequestHandler<GetAllWorkoutSets, IEnumerable<WorkoutSet>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkoutSetsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<WorkoutSet>> Handle(GetAllWorkoutSets request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkoutSetsRepository.GetAllWorkoutSets();
        }
    }
}
