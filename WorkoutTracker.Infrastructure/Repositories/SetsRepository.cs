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
    public class SetsRepository : ISetsRepository
    {
        WorkoutContext _workoutContext;

        public SetsRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }

        public async Task<Set> AddSet(Set set)
        {
            var res = await _workoutContext.Sets.AddAsync(set);
            return res.Entity;
        }

        public async Task<List<Set>> GetAllSets()
        {
            return await _workoutContext.Sets.ToListAsync();
        }

        public async Task<Set> GetSetById(int id)
        {
            return await _workoutContext.Sets.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateSet(Set set)
        {
            _workoutContext.Sets.Update(set);
        }
        public void DeleteSet(Set set)
        {
            _workoutContext.Sets.Remove(set);

        }
    }
}
