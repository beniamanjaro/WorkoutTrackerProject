using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Routines.Queries
{
    public class GetRoutineByIdHandler : IRequestHandler<GetRoutineById, Routine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoutineByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Routine> Handle(GetRoutineById request, CancellationToken cancellationToken)
        {
            var routine = await _unitOfWork.RoutinesRepository.GetRoutineById(request.Id);
            return routine;
        }
    }
}
