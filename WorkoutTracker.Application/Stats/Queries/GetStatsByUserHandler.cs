using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class GetStatsByUserHandler : IRequestHandler<GetStatsByUser, Dictionary<string, int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStatsByUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Dictionary<string,int>> Handle(GetStatsByUser request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            if (completedRoutines.Count == 0) return null;

            var statsHelper = new StatsHelper(completedRoutines);


            return statsHelper.GetStats();
        }
    }
}
