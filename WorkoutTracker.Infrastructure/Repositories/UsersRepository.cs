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
    public class UsersRepository : IUserRepository
    {
        WorkoutContext _workoutContext;

        public UsersRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }
        public async Task<User> AddUser(User user)
        {
            var newUser = await _workoutContext.Users.AddAsync(user);
            return newUser.Entity;

        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _workoutContext.Users.Include(u => u.WorkoutPlans)
                .Include(u => u.CompletedRoutines)
                .ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _workoutContext.Users.Include(u => u.WorkoutPlans)
                .ThenInclude(wp => wp.Routines)
                .ThenInclude(r => r.WorkoutSets)
                .ThenInclude(ws => ws.Sets)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByIdentityId(string id)
        {
            return await _workoutContext.Users.Include(u => u.WorkoutPlans)
                .FirstOrDefaultAsync(u => u.IdentityId == id);
        }

        public async Task UpdateUser(User user)
        {
            _workoutContext.Users.Update(user);
        }
        public async Task DeleteUser(User user)
        {
            _workoutContext.Users.Remove(user);
        }
    }
}
