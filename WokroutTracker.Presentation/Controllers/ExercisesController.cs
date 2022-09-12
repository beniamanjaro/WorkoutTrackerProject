using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Exercises.Commands;
using WorkoutTracker.Application.Exercises.Queries;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        public readonly ILogger _logger;

        public ExercisesController(IMediator mediator, IMapper mapper, ILogger<ExercisesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            _logger.LogInformation("Getting all exercises");

            var result = await _mediator.Send(new GetAllExercises());
            var mappedResult = _mapper.Map<List<ExerciseGetDto>>(result);

            _logger.LogInformation("Successfully retrieved the exercises");

            return Ok(mappedResult);

        }

        [HttpGet]
        [Route("{exerciseId}")]
        public async Task<IActionResult> GetExerciseById (int exerciseId)
        {
            _logger.LogInformation("Gettting exercise by id");

            var result = await _mediator.Send(new GetExerciseById { Id = exerciseId });
            if (result == null)
            {
                _logger.LogError("Coulnd't find the exercise with the id, {0}", exerciseId);

                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the exercise");

            var mappedResult = _mapper.Map<ExerciseGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetExercisesByCategory([FromQuery]string category)
        {
            _logger.LogInformation("Getting all exercises by category");

            var result = await _mediator.Send(new GetExercisesByCategory { Category = category });
            if (result == null)
            {
                _logger.LogError("Couldn't find any exercise with the selected category {0}", category);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the exercises");

            var mappedResult = _mapper.Map<List<ExerciseGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] ExercisePutPostDto exercise)
        {
            _logger.LogInformation("Creating a new exercise");

            var exerciseToAdd = await _mediator.Send(new CreateExercise
            {
                Category = exercise.Category,
                Equipment = exercise.Equipment,
                Name = exercise.Name
            });

            _logger.LogInformation("Successfully created the exercise");

            var mappedResult = _mapper.Map<ExerciseGetDto>(exerciseToAdd);


            return CreatedAtAction(nameof(GetExerciseById), new { Id = mappedResult.Id }, mappedResult);
        }

        [HttpDelete]
        [Route("{exerciseId}")]
        public async Task<IActionResult> DeleteExercise (int exerciseId)
        {
            _logger.LogInformation("Deleting an exercise");


            var result = await _mediator.Send(new DeleteExercise { Id = exerciseId });
            if (result == null)
            {
                _logger.LogError("Couldn't find the exercise with the id {0}", exerciseId);
                return NotFound();
            }

            _logger.LogInformation("Successfully deleted the exercise");

            return NoContent();
        }

    }
}
