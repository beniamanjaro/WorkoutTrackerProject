using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutSets.Commands
{
    public class DeleteWorkoutSetHandler : IRequestHandler<DeleteWorkoutSet, WorkoutSet>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkoutSetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutSet> Handle(DeleteWorkoutSet request, CancellationToken cancellationToken)
        {
            var workoutSetToDelete = await _unitOfWork.WorkoutSetsRepository.GetWorkoutSetById(request.Id);
            if (workoutSetToDelete == null) return null;
            
            _unitOfWork.WorkoutSetsRepository.DeleteWorkoutSet(workoutSetToDelete);
            await _unitOfWork.Save();

            return workoutSetToDelete;
        }
    }
}
