using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.Identity.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, UserManagerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterUserHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<UserManagerResponse> Handle(RegisterUser request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return new UserManagerResponse
                {
                    Message = "A user with the same email already exists.",
                    IsSuccess = false,
                };
            }

            var identityUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Username,
            };

            var createdUser = await _userManager.CreateAsync(identityUser, request.Password);

            if (createdUser.Succeeded)
            {
                await _unitOfWork.UsersRepository.AddUser(new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    IdentityId = identityUser.Id
                });
                await _unitOfWork.Save();

                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User couldn't be created",
                IsSuccess = false,
                Errors = createdUser.Errors.Select(e => e.Description),
            };


        }
    }
}
