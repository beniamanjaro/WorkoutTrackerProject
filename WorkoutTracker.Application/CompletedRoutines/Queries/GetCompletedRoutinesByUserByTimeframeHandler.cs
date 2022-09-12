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
    public class GetCompletedRoutinesByUserByTimeFrameHandler : IRequestHandler<GetCompletedRoutinesByUserByTimeframe, List<CompletedRoutine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompletedRoutinesByUserByTimeFrameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CompletedRoutine>> Handle(GetCompletedRoutinesByUserByTimeframe request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUserByTimeframe(request.UserId, request.TimeframeInMonths);

            return completedRoutines;
        }
    }
}
