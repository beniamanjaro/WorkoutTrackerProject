using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.CompletedRoutines.Commands;
using WorkoutTracker.Application.CompletedRoutines.Queries;
using WorkoutTracker.Application.Users.Commands;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompletedRoutinesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        public readonly ILogger _logger;

        public CompletedRoutinesController(IMediator mediator, IMapper mapper, ILogger<CompletedRoutinesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCompletedRoutineById(int id)
        {
            _logger.LogInformation("Getting completed routine by id.");

            var result = await _mediator.Send(new GetCompletedRoutineById() { Id = id });
            if (result == null)
            {
                _logger.LogWarning("Couldn't find the completed routine by id {0}", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved completed routine");

            var mappedResult = _mapper.Map<CompletedRoutineGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetCompletedRoutinesByUserId(int userId)
        {
            _logger.LogInformation("Getting completed routines by user id.");

            var result = await _mediator.Send(new GetCompletedRoutinesByUser() { UserId = userId });
            if (result == null)
            {
                _logger.LogWarning("Couldn't find the completed routines for user id {0}", userId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved completed routines");

            var mappedResult = _mapper.Map<List<CompletedRoutineGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("users/{userId}/workout-plans/{workoutPlanId}")]
        public async Task<IActionResult> GetCompletedRoutinesByUserByWorkoutPlan(int userId, int workoutPlanId)
        {
            _logger.LogInformation("Getting completed routines by user {0} for workout plan {1}.", userId, workoutPlanId);

            var result = await _mediator.Send(new GetCompletedRoutinesByUserByWorkoutPlan { UserId = userId, WorkoutPlanId = workoutPlanId });
            if (result == null)
            {
                _logger.LogWarning("Couldn't find the completed routines for user {0} for workout plan {1}", userId, workoutPlanId);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved completed routines");

            var mappedResult = _mapper.Map<List<CompletedRoutineGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompletedRoutine([FromBody] CompletedRoutinePutPostDto completedRoutine)
        {
            _logger.LogInformation("Adding a completed routine to database.");

            var mappedCompletedRoutine = _mapper.Map<CompletedRoutine>(completedRoutine);

            var result = await _mediator.Send(new AddCompletedRoutine
            {
                RoutineName = mappedCompletedRoutine.RoutineName,
                WorkoutPlanName = mappedCompletedRoutine.WorkoutPlanName,
                WorkoutPlanId = mappedCompletedRoutine.WorkoutPlanId,
                UserId = mappedCompletedRoutine.UserId,
                CreatedAt = mappedCompletedRoutine.CreatedAt,
                Exercises = mappedCompletedRoutine.Exercises,
                TotalReps = mappedCompletedRoutine.TotalReps,
                TotalSets = mappedCompletedRoutine.TotalSets,
                TotalVolume = mappedCompletedRoutine.TotalVolume,
            });

            _logger.LogInformation("Successfully added the completed routine");

            var mappedResult = _mapper.Map<CompletedRoutineGetDto>(result);
            return CreatedAtAction(nameof(GetCompletedRoutineById), new { Id = mappedResult.CompletedRoutineId }, mappedResult);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCompletedRoutine(int id)
        {
            _logger.LogInformation("Deleting completed routine");

            var result = await _mediator.Send(new DeleteCompletedRoutine() { Id = id });
            if (result == null)
            {
                _logger.LogWarning("Couldn't find the completed routine to delete by id, {0}", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully removed the completed routine");

            return NoContent();

        }
    }
}
