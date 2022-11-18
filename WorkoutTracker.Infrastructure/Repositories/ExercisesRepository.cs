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
    public class ExercisesRepository : IExercisesRepository
    {
        WorkoutContext _workoutContext;

        public ExercisesRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }


        public async Task<Exercise> AddExercise(Exercise exercise)
        {
            var newExercise = await _workoutContext.Exercises.AddAsync(exercise);
            return newExercise.Entity;
        }

        public async Task<List<Exercise>> AddExercises(List<Exercise> exercises)
        {
            _workoutContext.Exercises.AddRange(exercises);
            return exercises;
        }

        public async Task<Exercise> GetExerciseById(int id)
        {
            return await _workoutContext.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Exercise>> GetExercisesByName(string name, PaginationFilter paginationFilter)
        {
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;


            return await _workoutContext.Exercises
                .Where(e => e.Name.ToLower()
                .Contains(name)).Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToListAsync();
        }

        public async Task<List<Exercise>> GetExercisesByCategory(string category, PaginationFilter paginationFilter)
        {
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return await _workoutContext.Exercises
                .Where(e => e.Category.ToLower() == category.ToLower())
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToListAsync();
        }
        public async Task<List<Exercise>> GetAllExercisesWithNoPagination()
        {
            var exercises = await _workoutContext.Exercises.ToListAsync();
            return exercises;
        }

        public async Task<IList<ExerciseNameResponse>> GetExercisesNames()
        {
            return await _workoutContext.Exercises
                .Select(e => new ExerciseNameResponse { Name = e.Name, Id = e.Id})
                .Distinct().ToListAsync();
        }
        public async Task<List<string>> GetExercisesCategories()
        {
            return await _workoutContext.Exercises
                .Select(e => e.Category)
                .Distinct().ToListAsync();
        }
        public async Task<PagedList<Exercise>> GetAllExercises(PaginationFilter paginationFilter)
        {
            var exercises = await _workoutContext.Exercises.ToListAsync();

            return PagedList<Exercise>.ToPagedList(exercises.AsQueryable(), paginationFilter.PageNumber, paginationFilter.PageSize);
        }


        public void DeleteExercise(Exercise exercise)
        {
            _workoutContext.Exercises.Remove(exercise);
        }

        public async Task UpdateExercise(Exercise exercise)
        {
            _workoutContext.Exercises.Update(exercise);
        }

    }
}
