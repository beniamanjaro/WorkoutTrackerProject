using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.CompletedRoutines.Commands;
using WorkoutTracker.Application.CompletedRoutines.Queries;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedRoutinesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public CompletedRoutinesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetCompletedRoutineById(int id)
        {
            var result = await _mediator.Send(new GetCompletedRoutineById() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompletedRoutine(int userId, [FromBody] CompletedRoutinePutPostDto completedRoutine)
        {
            var mappedCompletedRoutine = _mapper.Map<CompletedRoutine>(completedRoutine);
            var result = await _mediator.Send(new AddCompletedRoutine
            {
                Name = mappedCompletedRoutine.Name,
                UserId = userId,
                WorkoutSets = mappedCompletedRoutine.WorkoutSets,
            });

            var mappedResult = _mapper.Map<CompletedRoutineGetDto>(result);
            return CreatedAtAction(nameof(GetCompletedRoutineById), new { Id = mappedResult.CompletedRoutineId }, mappedResult);
        }

    }
}
