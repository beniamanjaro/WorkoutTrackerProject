using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Sets.Commands
{
    public class AddSetHandler : IRequestHandler<AddSet, Set>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Set> Handle(AddSet request, CancellationToken cancellationToken)
        {
            var savedSet = await _unitOfWork.SetsRepository.AddSet(new Set { ExerciseId = request.ExerciseId, WorkoutSetId = request.WorkoutSetId, NumberOfReps = request.NumberOfReps, Weight = request.Weight });
            await _unitOfWork.Save();

            return savedSet;

        }
    }
}
