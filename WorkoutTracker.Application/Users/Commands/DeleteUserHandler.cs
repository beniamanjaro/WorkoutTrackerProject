using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Users.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var userToDelete = await _unitOfWork.UsersRepository.GetUserById(request.Id);
            if (userToDelete == null) return null;

            _unitOfWork.UsersRepository.DeleteUser(userToDelete);
            await _unitOfWork.Save();

            return userToDelete;

        }
    }
}
