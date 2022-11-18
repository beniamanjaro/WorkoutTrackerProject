using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Infrastructure.Repositories
{
    public class CompletedRoutinesRepository : ICompletedRoutinesRepository
    {
        private readonly WorkoutContext _workoutContext;

        public CompletedRoutinesRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }
        public async Task<CompletedRoutine> GetCompletedRoutineById(int id)
        {
            var completedRoutine = await _workoutContext.CompletedRoutines
                .Include(cr => cr.Exercises).ThenInclude(e => e.Exercise)
                .SingleOrDefaultAsync(cr => cr.CompletedRoutineId == id);
            return completedRoutine;
        }

        public async Task<List<CompletedRoutine>> GetCompletedRoutinesByUser(int userId)
        {
            var completedRoutines = await _workoutContext.CompletedRoutines
                .Where(cr => cr.UserId == userId)
                .Include(cr => cr.Exercises)
                .ThenInclude(e => e.Exercise)
                .ToListAsync();
            return completedRoutines;
        }
        public async Task<CompletedRoutine> AddCompletedRoutine(CompletedRoutine completedRoutine)
        {
            var completedRoutineToAdd = await _workoutContext.CompletedRoutines.AddAsync(completedRoutine);
            return completedRoutineToAdd.Entity;
        }

        public async Task<PagedList<CompletedRoutine>> GetCompletedRoutinesByUserByTimeframe(int userId, int timeframeInMonths, PaginationFilter paginationFilter)
        {
            var completedRoutines = _workoutContext.CompletedRoutines
                .Where(cr => cr.UserId == userId)
                .Where(cr => cr.CreatedAt > DateTime.Now.AddMonths(timeframeInMonths * -1))
                .OrderByDescending(cr => cr.CreatedAt);

            return PagedList<CompletedRoutine>.ToPagedList(completedRoutines, paginationFilter.PageNumber, paginationFilter.PageSize);
        }

        public async Task<PagedList<CompletedRoutine>> GetCompletedRoutinesByWorkoutPlanByUser(int userId, int workoutPlanId, PaginationFilter paginationFilter)
        {
            var completedRoutines = await GetCompletedRoutinesByUser(userId);
            var result = completedRoutines.Where(cr => cr.WorkoutPlanId == workoutPlanId).OrderByDescending(cr => cr.CreatedAt).AsQueryable();

            return PagedList<CompletedRoutine>.ToPagedList(result, paginationFilter.PageNumber, paginationFilter.PageSize);
        }

        public async Task<CompletedRoutine> GetMostRecentCompletedRoutinesExercisesStatsByUserByWorkoutPlanByName(int userId, int workoutPlanId, string routineName)
        {
            var completedRoutines = await GetCompletedRoutinesByUser(userId);
            var result = completedRoutines.Where(cr => cr.WorkoutPlanId == workoutPlanId && cr.RoutineName == routineName.ToLower()).OrderByDescending(cr => cr.CreatedAt).FirstOrDefault();

            return result;
        }

        public async Task<List<CompletedRoutine>> GetCompletedRoutinesByWorkoutPlan(int workoutPlanId)
        {
            var completedRoutines = await _workoutContext.CompletedRoutines.Where(cr => cr.WorkoutPlanId == workoutPlanId).ToListAsync();

            return completedRoutines;
        }

        public void DeleteCompletedRoutine(CompletedRoutine completedRoutine)
        {
            _workoutContext.CompletedRoutines.Remove(completedRoutine);
        }


        public async Task UpdateCompletedRoutine(CompletedRoutine completedRoutine)
        {
            _workoutContext.CompletedRoutines.Update(completedRoutine);
        }

    }
}
