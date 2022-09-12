using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Application.Identity.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUser, UserManagerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginUserHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<UserManagerResponse> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if(identityUser == null)
            {
                return new UserManagerResponse
                {
                    Message = "Wrong email or password",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(identityUser, request.Password);
            if(result == false)
            {
                return new UserManagerResponse
                {
                    Message = "Wrong email or password",
                    IsSuccess = false
                };
            }

            var user = await _unitOfWork.UsersRepository.GetUserByIdentityId(identityUser.Id);

            var claims = new[]
            {
                new Claim("Email", request.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Token = tokenAsString,
                IsSuccess = true,
                ExpiredDate = token.ValidTo,
                UserId = user.Id,
            };
        }
    }
}
