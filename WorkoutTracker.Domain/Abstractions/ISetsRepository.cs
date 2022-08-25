using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface ISetsRepository
    {
        Task<Set> AddSet(Set set);
        Task<Set> GetSetById(int id);
        Task<List<Set>> GetAllSets();
        public Task UpdateSet(Set set);
        public void DeleteSet(Set set);

    }
}
