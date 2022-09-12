using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Routines.Commands;
using WorkoutTracker.Application.Sets.Commands;
using WorkoutTracker.Application.WorkoutPlans.Commands;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Application.WorkoutSets.Commands;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WokroutTracker.Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutPlansController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        public readonly ILogger _logger;

        public WorkoutPlansController(IMediator mediator, IMapper mapper, ILogger<WorkoutPlansController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetWorkoutPlans()
        {
            _logger.LogInformation("Getting all the workout plans");

            var result = await _mediator.Send(new GetAllWorkoutPlans());
            var mappedResult = _mapper.Map<List<WorkoutPlanGetDto>>(result);

            _logger.LogInformation("Successfully retrieved the workout plans");

            return Ok(mappedResult);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWorkoutPlanById(int id)
        {
            _logger.LogInformation("Getting the workout plan with the id {0}", id);

            var result = await _mediator.Send(new GetWorkoutPlanById { Id = id });
            if (result == null)
            {
                _logger.LogError("Couldn't find the workout plan with the id {0}", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the workout plan");

            var mappedResult = _mapper.Map<WorkoutPlanGetDto>(result);
            return Ok(mappedResult);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWorkoutPlan([FromBody] WorkoutPlanPostDto workoutPlan)
        {
            _logger.LogInformation("Creating workout plan");

            var mappedWorkoutPlan = _mapper.Map<WorkoutPlan>(workoutPlan);
            var workoutPlanToAdd = await _mediator.Send(new CreateWorkoutPlan()
            {
                Name = mappedWorkoutPlan.Name,
                TimesPerWeek = mappedWorkoutPlan.TimesPerWeek,
                UserId = mappedWorkoutPlan.UserId,
                Routines = mappedWorkoutPlan.Routines
            });

            _logger.LogInformation("Successfully created the workout plan");

            var mappedResult = _mapper.Map<WorkoutPlanGetDto>(workoutPlanToAdd);
            return CreatedAtAction(nameof(GetWorkoutPlanById), new {Id = mappedResult.Id}, mappedResult);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWorkoutPlan(int id)
        {
            _logger.LogInformation("Deleting workout plan with the id {0}", id);

            var result = await _mediator.Send(new DeleteWorkoutPlan { Id = id });
            if (result == null)
            {
                _logger.LogError("Couldn't find the workout plan with the id {0}", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully deleted the workout plan");

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWorkoutPlan(int id,[FromBody] WorkoutPlanPutDto workoutPlan)
        {
            _logger.LogInformation("Updating workout plan with the id {0}", id);

            var result = await _mediator.Send(new UpdateWorkoutPlan {
                Name = workoutPlan.Name,
                Id = id,
                TimesPerWeek = workoutPlan.TimesPerWeek,
                Routines = workoutPlan.Routines,
                UserId = workoutPlan.UserId
            });

            _logger.LogInformation("Successfully updated the workout plan");

            var mappedResult = _mapper.Map<WorkoutPlanGetDto>(result);
            return Ok(mappedResult);
        }

    }
}
