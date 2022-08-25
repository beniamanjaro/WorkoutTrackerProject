using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Sets.Queries
{
    public class GetAllSetsHandler : IRequestHandler<GetAllSets, IEnumerable<Set>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSetsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Set>> Handle(GetAllSets request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.SetsRepository.GetAllSets();
        }
    }
}
