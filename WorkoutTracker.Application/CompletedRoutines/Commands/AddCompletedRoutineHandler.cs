using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Commands
{
    public class AddCompletedRoutineHandler : IRequestHandler<AddCompletedRoutine, CompletedRoutine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCompletedRoutineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CompletedRoutine> Handle(AddCompletedRoutine request, CancellationToken cancellationToken)
        {
            var completedRoutineToAdd = await _unitOfWork.CompletedRoutinesRepository.AddCompletedRoutine(new CompletedRoutine
            {
                RoutineName = request.RoutineName,
                WorkoutPlanName = request.WorkoutPlanName,
                WorkoutPlanId = request.WorkoutPlanId,
                CreatedAt = request.CreatedAt,
                UserId = request.UserId,
                TotalVolume = request.TotalVolume,
                TotalReps = request.TotalReps,
                TotalSets = request.TotalSets,
                Exercises = request.Exercises
            });

            await _unitOfWork.Save();
            return completedRoutineToAdd;

        }
    }
}
