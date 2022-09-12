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

        [HttpPost]
        public async Task<IActionResult> AddCompletedRoutine(int userId, [FromBody] CompletedRoutinePutPostDto completedRoutine)
        {
            _logger.LogInformation("Adding a completed routine to database.");

            var result = await _mediator.Send(new AddCompletedRoutine
            {
                Name = completedRoutine.Name,
                UserId = userId,
                RoutineId = completedRoutine.RoutineId,
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
