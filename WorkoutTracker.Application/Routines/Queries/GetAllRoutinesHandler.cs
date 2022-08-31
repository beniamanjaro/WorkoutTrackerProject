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
    public class GetAllRoutinesHandler : IRequestHandler<GetAllRoutines, IEnumerable<Routine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRoutinesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Routine>> Handle(GetAllRoutines request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RoutinesRepository.GetAllRoutines();
        }
    }
}
