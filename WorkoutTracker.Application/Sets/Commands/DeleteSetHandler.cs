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
    public class DeleteSetHandler : IRequestHandler<DeleteSet, Set>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Set> Handle(DeleteSet request, CancellationToken cancellationToken)
        {
            var setToDelete = await _unitOfWork.SetsRepository.GetSetById(request.Id);
            if(setToDelete == null) return null;

            _unitOfWork.SetsRepository.DeleteSet(setToDelete);
            await _unitOfWork.Save();

            return setToDelete;
        }
    }
}
