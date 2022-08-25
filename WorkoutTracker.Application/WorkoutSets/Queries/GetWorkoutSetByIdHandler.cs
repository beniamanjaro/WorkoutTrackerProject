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
    public class GetWorkoutSetByIdHandler : IRequestHandler<GetWorkoutSetById, WorkoutSet>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWorkoutSetByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<WorkoutSet> Handle(GetWorkoutSetById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkoutSetsRepository.GetWorkoutSetById(request.Id);
        }
    }
}
