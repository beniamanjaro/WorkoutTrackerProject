using Google.Apis.Auth;
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
    public class GoogleLoginRegisterHandler : IRequestHandler<GoogleLoginRegister, UserManagerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public GoogleLoginRegisterHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
        }



        public async Task<UserManagerResponse> Handle(GoogleLoginRegister request, CancellationToken cancellationToken)
        {
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            if(validPayload == null)
            {
                return new UserManagerResponse
                {
                    Message = "Google token is not valid",
                    IsSuccess = false
                };
            }


            var identityUser = await _userManager.FindByEmailAsync(validPayload.Email);
            if (identityUser == null)
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
                new Claim("Email", validPayload.Email),
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
                Message = "Signed in with google",
                IsSuccess = true,
                ExpiredDate = token.ValidTo,
                UserId = user.Id,
            };

        }
    }
}
