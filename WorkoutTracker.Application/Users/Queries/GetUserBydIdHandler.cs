using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Users.Queries
{
    public class GetUserBydIdHandler : IRequestHandler<GetUserBydId, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserBydIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetUserBydId request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UsersRepository.GetUserById(request.Id);
        }
    }
}
