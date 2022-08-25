using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Commands
{
    public class CreateWorkoutPlanHandler : IRequestHandler<CreateWorkoutPlan, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateWorkoutPlan request, CancellationToken cancellationToken)
        {
            var savedWorkoutPlan = await _unitOfWork.WorkoutPlansRepository.AddWorkoutPlan(new WorkoutPlan { UserId = request.UserId, Name = request.Name, TimesPerWeek = request.TimesPerWeek });
            await _unitOfWork.Save();
            var savedWorkoutPlanId = savedWorkoutPlan.Id;
            foreach(var routine in request.Routines)
            {
                var savedRoutine = await _unitOfWork.RoutinesRepository.CreateRoutine(new Routine() {Name = routine.Name, DayOrderNumber = routine.DayOrderNumber, WorkoutPlanId = savedWorkoutPlanId });
                await _unitOfWork.Save();
                var savedRoutineId = savedRoutine.Id;
                foreach(var workoutSet in routine.WorkoutSets)
                {
                    var savedWorkoutSet = await _unitOfWork.WorkoutSetsRepository.AddWorkoutSet(new WorkoutSet() {RoutineId = savedRoutineId });
                    await _unitOfWork.Save();
                    var savedWorkoutSetId = savedWorkoutSet.Id;
                    foreach(var set in workoutSet.Sets)
                    {
                        await _unitOfWork.SetsRepository.AddSet(new Set() { ExerciseId = set.ExerciseId, NumberOfReps = set.NumberOfReps, Weight = set.Weight, WorkoutSetId = savedWorkoutSetId});
                        await _unitOfWork.Save();
                    }
                }
            }

            return savedWorkoutPlanId;

        }

    }
}
