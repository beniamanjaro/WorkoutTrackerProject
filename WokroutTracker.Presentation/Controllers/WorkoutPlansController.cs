using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Exercises.Queries;
using WorkoutTracker.Application.Routines.Commands;
using WorkoutTracker.Application.Sets.Commands;
using WorkoutTracker.Application.Stats.Queries;
using WorkoutTracker.Application.WorkoutPlans.Commands;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Application.WorkoutSets.Commands;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;
using WorkoutTracker.Presentation.Responses;

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
        [Route("{id}/top-users")]
        public async Task<IActionResult> GetTopUsersForWorkoutPlan(int id)
        {
            _logger.LogInformation("Getting top users for the workout plan");

            var result = await _mediator.Send(new GetTopUsersForWorkoutPlan() { Id = id});
            if(result == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the users");

            return Ok(result);

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

        [HttpGet]
        [Route("{id}/muscle-split")]
        public async Task<IActionResult> GetMusclesplitForWorkoutPlan(int id)
        {
            _logger.LogInformation("Getting the workout plan with the id {0}", id);

            var result = await _mediator.Send(new GetMuscleSplitForRoutinesByWorkoutPlan { Id = id });
            if (result == null)
            {
                _logger.LogError("Couldn't find the workout plan with the id {0}", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the workout plan");

            return Ok(result);
        }

        [HttpGet]
        [Route("most-popular-workout-plans")]
        public async Task<IActionResult> GetMostUsedWorkoutPlans([FromQuery] PaginationDto paginationData)
        {

            _logger.LogInformation("Getting most popular workout plans");

            var paginationFilter = _mapper.Map<PaginationFilter>(paginationData);

            var workoutPlans = await _mediator.Send(new GetMostUsedWorkoutPlans { PaginationFilter = paginationFilter });
            var mappedWorkoutPlans = _mapper.Map<List<WorkoutPlanGetDto>>(workoutPlans);

            var paginationResponse = new PagedResponse<WorkoutPlanGetDto>(mappedWorkoutPlans);
            paginationResponse.PageSize = paginationFilter.PageSize;
            paginationResponse.PageNumber = paginationFilter.PageNumber;

            _logger.LogInformation("Successfully retrieved the workout plans");

            return Ok(paginationResponse);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetWorkoutPlansBySearch([FromQuery] PaginationDto paginationData, [FromQuery] FilterSortData filterSortData, [FromQuery] string? searchValue = "")
        {
            _logger.LogInformation("Getting all exercises by name");

            var paginationFilter = _mapper.Map<PaginationFilter>(paginationData);

            var workoutPlans = await _mediator.Send(new GetFilteredSortedWorkoutPlans { SearchValue = searchValue, PaginationFilter = paginationFilter, FilterSortData = filterSortData });

            if (workoutPlans == null)
            {
                _logger.LogError("Couldn't find any exercise with the selected name {0}", searchValue);
                return NotFound();
            }

            var mappedWorkoutPlans = _mapper.Map<List<WorkoutPlanGetDto>>(workoutPlans);
            var paginationResponse = new PagedResponse<WorkoutPlanGetDto>(mappedWorkoutPlans);
            paginationResponse.Data = mappedWorkoutPlans;
            paginationResponse.TotalPages = workoutPlans.TotalPages;
            paginationResponse.HasNext = workoutPlans.HasNext;
            paginationResponse.HasPrevious = workoutPlans.HasPrevious;
            paginationResponse.PageSize = paginationFilter.PageSize;
            paginationResponse.PageNumber = paginationFilter.PageNumber;

            _logger.LogInformation("Successfully retrieved the exercises");

            return Ok(paginationResponse);
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
        public async Task<IActionResult> UpdateWorkoutPlan(int id,[FromBody] WorkoutPlanPostDto workoutPlan)
        {
            _logger.LogInformation("Updating workout plan with the id {0}", id);

            var mappedWorkoutPlan = _mapper.Map<WorkoutPlan>(workoutPlan);

            var result = await _mediator.Send(new UpdateWorkoutPlan {
                Name = mappedWorkoutPlan.Name,
                Id = id,
                TimesPerWeek = mappedWorkoutPlan.TimesPerWeek,
                Routines = mappedWorkoutPlan.Routines,
                UserId = mappedWorkoutPlan.UserId
            });

            _logger.LogInformation("Successfully updated the workout plan");

            var mappedResult = _mapper.Map<WorkoutPlanGetDto>(result);
            return Ok(mappedResult);
        }

    }
}
