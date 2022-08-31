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
    public class CreateWorkoutPlanHandler : IRequestHandler<CreateWorkoutPlan, WorkoutPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkoutPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlan> Handle(CreateWorkoutPlan request, CancellationToken cancellationToken)
        {
            var savedWorkoutPlan = await _unitOfWork.WorkoutPlansRepository.AddWorkoutPlan(new WorkoutPlan
            { UserId = request.UserId,
                Name = request.Name,
                TimesPerWeek = request.TimesPerWeek,
                Routines = request.Routines
            });
            await _unitOfWork.Save();

            return savedWorkoutPlan;

        }

    }
}
