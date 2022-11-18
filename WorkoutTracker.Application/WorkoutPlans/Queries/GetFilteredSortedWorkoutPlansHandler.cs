using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Application.WorkoutPlans.Queries
{
    public class GetFilteredSortedWorkoutPlansHandler : IRequestHandler<GetFilteredSortedWorkoutPlans, PagedList<WorkoutPlan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetFilteredSortedWorkoutPlansHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<WorkoutPlan>> Handle(GetFilteredSortedWorkoutPlans request, CancellationToken cancellationToken)
        {
            var timesPerWeekFilter = request.FilterSortData.TimesPerWeekFilter;
            var workoutPlans = new List<WorkoutPlan>();

            if (!String.IsNullOrEmpty(request.SearchValue))
            {
                workoutPlans = await _unitOfWork.WorkoutPlansRepository.GetAllWorkoutPlansBySearchValue(request.SearchValue);
            } else
            {
                workoutPlans = await _unitOfWork.WorkoutPlansRepository.GetAllWorkoutPlansBySearchValue("");
            }

            // Filters the workout plans according to the filter value
            if (timesPerWeekFilter != null)
            {
                workoutPlans = workoutPlans.Where(wp => wp.TimesPerWeek == timesPerWeekFilter).ToList();
            }




            // Sorts the workout plans by the sortBy value
            if (!String.IsNullOrEmpty(request.FilterSortData.SortBy))
            {
            switch (request.FilterSortData.SortBy.ToLower())
            {
                case "popularity":
                    workoutPlans = workoutPlans.OrderByDescending(wp => wp.Users.Count()).ToList();
                    break;
                case "recent":
                    workoutPlans = workoutPlans.OrderByDescending(wp => wp.CreatedAt).ToList();
                    break;
            }
            }

            return PagedList<WorkoutPlan>.ToPagedList(workoutPlans.AsQueryable(), request.PaginationFilter.PageNumber, request.PaginationFilter.PageSize);
        }
    }
}
