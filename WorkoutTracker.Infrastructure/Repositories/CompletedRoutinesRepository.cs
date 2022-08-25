using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Infrastructure.Repositories
{
    public class CompletedRoutinesRepository
    {
        private readonly WorkoutContext _workoutContext;

        public CompletedRoutinesRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }

        public void AddCompletedRoutine(int userId, int routineId)
        {

        }
        
    }
}
