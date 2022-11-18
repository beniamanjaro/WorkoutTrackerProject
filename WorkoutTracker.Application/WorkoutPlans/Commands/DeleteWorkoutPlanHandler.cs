using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Commands
{
    public class DeleteWorkoutPlanHandler : IRequestHandler<DeleteWorkoutPlan, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlan> Handle(DeleteWorkoutPlan request, CancellationToken cancellationToken)
        {
            var workoutPlanToDelete = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.Id);
            var completedRoutines = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutinesByWorkoutPlan(workoutPlanToDelete.Id);

            foreach(var cr in completedRoutines)
            {
                cr.WorkoutPlanId = null;
            }

            _unitOfWork.WorkoutPlansRepository.DeleteWorkoutPlan(workoutPlanToDelete);
            await _unitOfWork.Save();

            return workoutPlanToDelete;

        }
    }
}
