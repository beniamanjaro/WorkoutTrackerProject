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
    public class AddWorkoutSetHandler : IRequestHandler<AddWorkoutSet, WorkoutSet>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddWorkoutSetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<WorkoutSet> Handle(AddWorkoutSet request, CancellationToken cancellationToken)
        {
            var savedWorkoutSet = await _unitOfWork.WorkoutSetsRepository.AddWorkoutSet(new WorkoutSet { RoutineId = request.RoutineId });
            await _unitOfWork.Save();

            return savedWorkoutSet;
        }
    }
}
