using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Queries
{
    public class GetCompletedRoutinesByUserHandler : IRequestHandler<GetCompletedRoutinesByUser, IEnumerable<CompletedRoutine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompletedRoutinesByUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CompletedRoutine>> Handle(GetCompletedRoutinesByUser request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
        }
    }
}
