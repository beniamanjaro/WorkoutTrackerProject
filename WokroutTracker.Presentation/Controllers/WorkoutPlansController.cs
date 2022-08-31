using AutoMapper;
using MediatR;
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
    public class WorkoutPlans : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public WorkoutPlans(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetWorkoutPlans()
        {
            var result = await _mediator.Send(new GetAllWorkoutPlans());
            var mappedResult = _mapper.Map<List<WorkoutPlanGetDto>>(result);

            return Ok(mappedResult);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWorkoutPlanById(int id)
        {
            var result = await _mediator.Send(new GetWorkoutPlanById { Id = id});
            var mappedResult = _mapper.Map<WorkoutPlanGetDto>(result);

            return Ok(mappedResult);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWorkoutPlan([FromBody] WorkoutPlanPutPostDto workoutPlan)
        {
            var mappedWorkoutPlan = _mapper.Map<WorkoutPlan>(workoutPlan);
            var workoutPlanToAdd = await _mediator.Send(new CreateWorkoutPlan()
            {
                Name = mappedWorkoutPlan.Name,
                TimesPerWeek = mappedWorkoutPlan.TimesPerWeek,
                UserId = mappedWorkoutPlan.UserId,
                Routines = mappedWorkoutPlan.Routines
            });

            var mappedResult = _mapper.Map<WorkoutPlanGetDto>(workoutPlanToAdd);
            return CreatedAtAction(nameof(GetWorkoutPlanById), new {Id = mappedResult.Id}, mappedResult);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWorkoutPlan(int id)
        {
            var result = await _mediator.Send(new DeleteWorkoutPlan { Id = id });
            if (result == null) return NotFound();

            return NoContent();
        }



    }
}
