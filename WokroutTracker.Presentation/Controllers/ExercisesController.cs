using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Exercises.Commands;
using WorkoutTracker.Application.Exercises.Queries;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.DTOs;
using WorkoutTracker.Presentation.Responses;

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
        public async Task<IActionResult> GetAllExercises([FromQuery] PaginationDto paginationData)
        {
            _logger.LogInformation("Getting all exercises");

            var paginationFilter = _mapper.Map<PaginationFilter>(paginationData);

            var exercises = await _mediator.Send(new GetAllExercises { PaginationFilter = paginationFilter});
            var mappedExercises = _mapper.Map<List<ExerciseGetDto>>(exercises);

            var paginationResponse = new PagedResponse<ExerciseGetDto>();
            paginationResponse.Data = mappedExercises;
            paginationResponse.TotalPages = exercises.TotalPages;
            paginationResponse.HasNext = exercises.HasNext;
            paginationResponse.HasPrevious = exercises.HasPrevious;
            paginationResponse.PageSize = paginationFilter.PageSize;
            paginationResponse.PageNumber = paginationFilter.PageNumber;

            _logger.LogInformation("Successfully retrieved the exercises");

            return Ok(paginationResponse);

        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetExercisesByName(string name,[FromQuery] PaginationDto paginationData)
        {
            _logger.LogInformation("Getting all exercises by name");

            var paginationFilter = _mapper.Map<PaginationFilter>(paginationData);

            var exercises = await _mediator.Send(new GetExercisesByName { Name = name, PaginationFilter = paginationFilter });

            if (exercises == null)
            {
                _logger.LogError("Couldn't find any exercise with the selected name {0}", name);
                return NotFound();
            }

            var mappedExercises = _mapper.Map<List<ExerciseGetDto>>(exercises);
            var paginationResponse = new PagedResponse<ExerciseGetDto>(mappedExercises);
            paginationResponse.PageSize = paginationFilter.PageSize;
            paginationResponse.PageNumber = paginationFilter.PageNumber;

            _logger.LogInformation("Successfully retrieved the exercises");

            return Ok(paginationResponse);
        }

        [HttpGet]
        [Route("{exerciseId}")]
        public async Task<IActionResult> GetExerciseById (int exerciseId)
        {
            _logger.LogInformation("Gettting exercise by id");

            var exercise = await _mediator.Send(new GetExerciseById { Id = exerciseId });
            if (exercise == null)
            {
                _logger.LogError("Coulnd't find the exercise with the id, {0}", exerciseId);

                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the exercise");

            var mappedExercise = _mapper.Map<ExerciseGetDto>(exercise);
            return Ok(mappedExercise);
        }

        [HttpGet]
        [Route("{exerciseId}/similar-exercises")]
        public async Task<IActionResult> GetSimilarExercises(int exerciseId)
        {
            _logger.LogInformation("Gettting exercise by id");

            var exercises = await _mediator.Send(new GetSimilarExercises { ExerciseId = exerciseId });
            if (exercises == null)
            {
                _logger.LogError("Coulnd't find the exercise with the id, {0}", exerciseId);

                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the exercise");

            var mappedExercise = _mapper.Map<List<ExerciseGetDto>>(exercises);
            return Ok(mappedExercise);
        }

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetExercisesByCategory(string category, [FromQuery] PaginationDto paginationData)
        {
            _logger.LogInformation("Getting all exercises by category");

            var paginationFilter = _mapper.Map<PaginationFilter>(paginationData);

            var exercises = await _mediator.Send(new GetExercisesByCategory { Category = category, PaginationFilter = paginationFilter });
            if (exercises == null)
            {
                _logger.LogError("Couldn't find any exercise with the selected category {0}", category);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the exercises");

            var mappedExercises = _mapper.Map<List<ExerciseGetDto>>(exercises);
            var paginationResponse = new PagedResponse<ExerciseGetDto>(mappedExercises);
            paginationResponse.PageSize = paginationFilter.PageSize;
            paginationResponse.PageNumber = paginationFilter.PageNumber;

            return Ok(paginationResponse);
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetExerciseCategories()
        {
            _logger.LogInformation("Getting all exercise categories");

            var categories = await _mediator.Send(new GetExerciseCategories());
            if (categories == null)
            {
                _logger.LogError("Couldn't find any category");
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the categories");

            return Ok(categories);
        }

        [HttpGet]
        [Route("names")]
        public async Task<IActionResult> GetExercisesNames()
        {
            _logger.LogInformation("Getting all exercises names");

            var names = await _mediator.Send(new GetExercisesNames());
            if (names == null)
            {
                _logger.LogError("Couldn't find any exercise name");
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved the exercises names");

            return Ok(names);
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


            return CreatedAtAction(nameof(GetExerciseById), new { exerciseId = mappedResult.Id }, mappedResult);
        }

        [HttpPost]
        [Route("add-exercises")]
        public async Task<IActionResult> CreateExercises([FromBody] List<ExercisePutPostDto> exercises)
        {
            _logger.LogInformation("Creating a new exercises");
            var mappedExercises = _mapper.Map<List<Exercise>>(exercises);

            var exercisesToAdd = await _mediator.Send(new CreateExercises
            {
                Exercises = mappedExercises
            });

            _logger.LogInformation("Successfully created the exercises");

            return Ok(exercisesToAdd);
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
