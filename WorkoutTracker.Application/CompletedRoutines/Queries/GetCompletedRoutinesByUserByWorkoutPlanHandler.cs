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
    public class GetCompletedRoutinesByUserByWorkoutPlanHandler : IRequestHandler<GetCompletedRoutinesByUserByWorkoutPlan, List<CompletedRoutine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompletedRoutinesByUserByWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CompletedRoutine>> Handle(GetCompletedRoutinesByUserByWorkoutPlan request, CancellationToken cancellationToken)
        {
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByUser(request.UserId);
            var result = completedRoutines.Where(cr => cr.WorkoutPlanId == request.WorkoutPlanId).OrderByDescending(cr => cr.CreatedAt).ToList();

            return result;
        }
    }
}
