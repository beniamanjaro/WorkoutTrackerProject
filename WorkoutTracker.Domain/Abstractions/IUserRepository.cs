using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByIdentityId(string id);
        Task UpdateUser(User user);
        void DeleteUser(User user);

    }
}
