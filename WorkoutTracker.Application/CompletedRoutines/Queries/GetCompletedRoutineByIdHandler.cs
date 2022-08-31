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
    public class GetCompletedRoutineByIdHandler : IRequestHandler<GetCompletedRoutineById, CompletedRoutine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompletedRoutineByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CompletedRoutine> Handle(GetCompletedRoutineById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CompletedRoutinesRepository.GetCompletedRoutineById(request.Id);
        }
    }
}
