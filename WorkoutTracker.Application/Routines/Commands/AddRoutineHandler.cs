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
    public class AddRoutineHandler : IRequestHandler<AddRoutine, Routine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRoutineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Routine> Handle(AddRoutine request, CancellationToken cancellationToken)
        {
            var routineToAdd = await _unitOfWork.RoutinesRepository.AddRoutine(new Routine {WorkoutPlanId = request.WorkoutPlanId, DayOrderNumber = request.DayOrderNumber, Name = request.Name });
            await _unitOfWork.Save();

            return routineToAdd;
        }
    }
}
