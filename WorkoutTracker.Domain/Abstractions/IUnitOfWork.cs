using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        public IExercisesRepository ExercisesRepository { get; }
        public IRoutinesRepository RoutinesRepository { get; }
        public IWorkoutPlansRepository WorkoutPlansRepository { get; }
        public IWorkoutSetsRepository WorkoutSetsRepository { get; }
        public ISetsRepository SetsRepository { get; }
        public IUserRepository UsersRepository { get; }
        public ICompletedRoutinesRepository CompletedRoutinesRepository { get; }
        public Task Save();


    }
}
