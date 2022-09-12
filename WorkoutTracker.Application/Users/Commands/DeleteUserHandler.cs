using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteUserHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<User> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var userToDelete = await _unitOfWork.UsersRepository.GetUserById(request.Id);
            if (userToDelete == null) return null;

            var userIdentityToDelete = await _userManager.FindByEmailAsync(userToDelete.Email);
            await _userManager.DeleteAsync(userIdentityToDelete);

            _unitOfWork.UsersRepository.DeleteUser(userToDelete);
            await _unitOfWork.Save();

            return userToDelete;

        }
    }
}
