using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Exercises.Commands;
using WorkoutTracker.Application.Exercises.Queries;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public ExercisesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var result = await _mediator.Send(new GetAllExercises());
            var mappedResult = _mapper.Map<List<ExerciseGetDto>>(result);

            return Ok(mappedResult);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExerciseById (int id)
        {
            var result = await _mediator.Send(new GetExerciseById { Id = id});
            var mappedResult = _mapper.Map<ExerciseGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] ExercisePostDto exercise)
        {
            var exerciseToAdd = await _mediator.Send(new CreateExercise
            {
                Category = exercise.Category,
                Equipment = exercise.Equipment,
                Name = exercise.Name
            });

            var mappedResult = _mapper.Map<ExerciseGetDto>(exerciseToAdd);


            return CreatedAtAction(nameof(GetExerciseById), new { Id = mappedResult.Id }, mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteExercise (int id)
        {
            var result = await _mediator.Send(new DeleteExercise { Id = id });
            if (result == null) return NotFound();

            return NoContent();
        }



    }
}
