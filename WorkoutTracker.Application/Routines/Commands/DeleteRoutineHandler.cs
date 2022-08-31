using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Routines.Commands
{
    public class DeleteRoutineHandler : IRequestHandler<DeleteRoutine, Routine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoutineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Routine> Handle(DeleteRoutine request, CancellationToken cancellationToken)
        {
            var routineToDelete = await _unitOfWork.RoutinesRepository.GetRoutineById(request.Id);
            if (routineToDelete == null) return null;

            _unitOfWork.RoutinesRepository.DeleteRoutine(routineToDelete);
            await _unitOfWork.Save();

            return routineToDelete;


        }
    }
}
