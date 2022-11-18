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
    public class GetCompletedRoutinesByUserByTimeFrameHandler : IRequestHandler<GetCompletedRoutinesByUserByTimeframe, PagedList<CompletedRoutine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompletedRoutinesByUserByTimeFrameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<CompletedRoutine>> Handle(GetCompletedRoutinesByUserByTimeframe request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUserByTimeframe(request.UserId, request.TimeframeInMonths, request.PaginationFilter);

            return completedRoutines;
        }
    }
}
