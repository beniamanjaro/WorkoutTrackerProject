using AutoMapper;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WorkoutTracker.Application.Identity.Commands;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    public class AuthController : Controller
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        public readonly ILogger _logger;

        public AuthController(IMediator mediator, IMapper mapper, ILogger<CompletedRoutinesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        {
            _logger.LogInformation("Registering a new user.");

            var result = await _mediator.Send(new RegisterUser()
            {
                Email = model.Email,
                Password = model.Password,
                Username = model.Username
            });

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            _logger.LogError("Couldn't register the user");
            foreach(var err in result.Errors)
            {
                _logger.LogError(err);
            }

            return BadRequest(result);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto model)
        {
            _logger.LogInformation("Logging in a user.");

            var result = await _mediator.Send(new LoginUser()
            {
                Email = model.Email,
                Password = model.Password,
            });

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            _logger.LogError("User couldn't log in.");
            _logger.LogError(result.Message);

            return BadRequest(result);

        }

        [HttpPost]
        [Route("google")]
        public async Task<IActionResult> LoginGoogle([FromBody] GoogleLoginRegisterDto model)
        {
            var result = await _mediator.Send(new GoogleLoginRegister { IdToken = model.IdToken});

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            _logger.LogError("User couldn't log in.");
            _logger.LogError(result.Message);

            return BadRequest(result);


        }
    }
}
