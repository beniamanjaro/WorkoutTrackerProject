using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Exercises.Queries;
using WorkoutTracker.Application.Notifications.Commands;
using WorkoutTracker.Presentation.DTOs;

namespace WorkoutTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        public readonly ILogger _logger;

        public NotificationsController(IMediator mediator, IMapper mapper, ILogger<ExercisesController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("workout-plan")]
        public async Task<IActionResult> CreateNotificationForUsersSubscribedToWorkoutPlan([FromBody]int workoutPlanId, string message)
        {
            _logger.LogInformation("Gettting exercise by id");

            var notifications = await _mediator.Send(new CreateNotification { Message = message, WorkoutPlanId = workoutPlanId });
            if (notifications == null)
            {
                _logger.LogError("No users to send notification to");

                return NotFound();
            }

            _logger.LogInformation("Successfully sent the notifications");

            //var mappedExercise = _mapper.Map<List<ExerciseGetDto>>(exercises);
            return Ok(notifications);
        }
    }
}
