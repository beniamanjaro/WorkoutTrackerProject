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
    public class CreateUserHandler : IRequestHandler<CreateUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var savedUser = await _unitOfWork.UsersRepository.AddUser(new User {Username = request.Username, Email = request.Email  });
            await _unitOfWork.Save();

            return savedUser;
        }
    }
}
