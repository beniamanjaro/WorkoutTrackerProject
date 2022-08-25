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
    public class UpdateWorkoutPlanHandler : IRequestHandler<UpdateWorkoutPlan, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlan> Handle(UpdateWorkoutPlan request, CancellationToken cancellationToken)
        {
            var workoutPlanToUpdate = await _unitOfWork.WorkoutPlansRepository.GetWorkoutPlanById(request.Id);
            if (request.Name != null)
            {
            workoutPlanToUpdate.Name = request.Name;

            }
            if(request.TimesPerWeek != 0)
            {
            workoutPlanToUpdate.TimesPerWeek = request.TimesPerWeek;
            }

            await _unitOfWork.WorkoutPlansRepository.UpdateWorkoutPlan(workoutPlanToUpdate);
            await _unitOfWork.Save();

            return workoutPlanToUpdate;

        }
    }
}
