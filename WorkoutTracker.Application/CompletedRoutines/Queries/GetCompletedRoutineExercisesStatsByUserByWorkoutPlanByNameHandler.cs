using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.CompletedRoutines.Queries
{
    public class GetCompletedRoutineExercisesStatsByUserByWorkoutPlanByNameHandler : IRequestHandler<GetCompletedRoutineExercisesStatsByUserByWorkoutPlanByName, List<CompletedRoutineExercise>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompletedRoutineExercisesStatsByUserByWorkoutPlanByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CompletedRoutineExercise>> Handle(GetCompletedRoutineExercisesStatsByUserByWorkoutPlanByName request, CancellationToken cancellationToken)
        {
            var completedRoutine = await _unitOfWork.CompletedRoutinesRepository.GetMostRecentCompletedRoutinesExercisesStatsByUserByWorkoutPlanByName(request.UserId,request.WorkoutPlanId,request.RoutineName);

            if(completedRoutine == null)
            {
                return null;
            }

            var exercises = completedRoutine.Exercises.ToList();

            return exercises;
        }
    }
}
