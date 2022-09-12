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
    public class DeleteCompletedRoutineHandler : IRequestHandler<DeleteCompletedRoutine, CompletedRoutine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCompletedRoutineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CompletedRoutine> Handle(DeleteCompletedRoutine request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutineById(request.Id);
            _unitOfWork.CompletedRoutinesRepository.DeleteCompletedRoutine(result);

            await _unitOfWork.Save();

            return result;
        }

    }
}
