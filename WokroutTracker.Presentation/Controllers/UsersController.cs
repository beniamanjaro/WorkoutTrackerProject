using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.CompletedRoutines.Queries;
using WorkoutTracker.Application.Stats.Queries;
using WorkoutTracker.Application.Users.Commands;
using WorkoutTracker.Application.Users.Queries;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        public readonly ILogger _logger;

        public UsersController(IMediator mediator, IMapper mapper, ILogger<ExercisesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            _logger.LogInformation("Getting user by id");

            var result = await _mediator.Send(new GetUserBydId() { Id = userId });
            if (result == null)
            {
                _logger.LogError("Couldn't find the user with the id {0}", userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the user");

            var mappedResult = _mapper.Map<UserGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{userId}/workout-plans")]
        public async Task<IActionResult> GetWorkoutPlansByUserId(int userId)
        {
            _logger.LogInformation("Getting the workout plans for the user with id {0}", userId);

            var result = await _mediator.Send(new GetWorkoutPlansByUser() { UserId = userId });
            if (result == null)
            {
                _logger.LogError("Couldn't find any workout plans for the specified user with id", userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the workout plans");

            var mappedResult = _mapper.Map<List<WorkoutPlanGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{userId}/completed-routines")]
        public async Task<IActionResult> GetCompletedRoutinesByUserId(int userId)
        {
            _logger.LogInformation("Getting the completed routines for the user with id {0}", userId);

            var result = await _mediator.Send(new GetCompletedRoutinesByUser() { UserId = userId });
            if (result == null)
            {
                _logger.LogError("Couldn't find any completed routines for the specified user with id", userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the completed routines");


            var mappedResult = _mapper.Map<List<CompletedRoutineGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{userId}/history")]
        public async Task<IActionResult> GetCompletedRoutinesByTimeFrame(int userId, [FromQuery]int timeframeInMonths)
        {
            _logger.LogInformation("Getting the completed routines for the user with id {0} in the past {1} months", userId, timeframeInMonths);

            var result = await _mediator.Send(new GetCompletedRoutinesByUserByTimeframe() {UserId = userId, TimeframeInMonths = timeframeInMonths });
            if (result == null)
            {
                _logger.LogError("Couldn't find any completed routines in the selected timeframe");
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the completed routines");

            var mappedResult = _mapper.Map<List<CompletedRoutineGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Getting all users");

            var result = await _mediator.Send(new GetAllUsers());
            if (result == null)
            {
                _logger.LogError("No user could be found");
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the users");

            var mappedResult = _mapper.Map<List<UserGetDto>>(result);
            return Ok(mappedResult);

        }

        [HttpGet]
        [Route("{userId}/analytics")]
        public async Task<IActionResult> GetUserStatsById(int userId)
        {
            _logger.LogInformation("Getting the stats for the user with the id {0}", userId);

            var stats = await _mediator.Send(new GetStatsByUser { UserId = userId });
            if (stats == null)
            {
                _logger.LogError("Couldn't get the stats for the user with the id {0}", userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the stats");


            return Ok(stats);
        }

        [HttpGet]
        [Route("{userId}/analytics/{exercise-name}")]
        public async Task<IActionResult> GetExerciseStatsByUserByName(int userId, string exerciseName)
        {
            _logger.LogInformation("Getting the stats for the exercise {0} for the user with the id {1}", exerciseName, userId);

            var stats = await _mediator.Send(new GetExerciseStatsByUser { UserId = userId, ExerciseName = exerciseName });
            if (stats == null)
            {
                _logger.LogError("Couldn't get the stats for the exercise {0} for the user with the id {1}", exerciseName, userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the stats for the exercise");

            return Ok(stats);
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            _logger.LogInformation("Deleting the user with the id {0}", userId);

            var result = await _mediator.Send(new DeleteUser() { Id = userId });
            if (result == null)
            {
                _logger.LogError("Couldn't delete the user with the id {0}", userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully deleted the user");

            return NoContent();

        }

    }
}
