using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkoutContext _dataContext;
        public UnitOfWork(WorkoutContext dataContext, IExercisesRepository exercisesRepository,
            IRoutinesRepository routinesRepository, IWorkoutPlansRepository workoutPlansRepository,
            IWorkoutSetsRepository workoutSetsRepository, ISetsRepository setsRepository, IUserRepository usersRepository,
            ICompletedRoutinesRepository completedRoutinesRepository )
        {
            _dataContext = dataContext;
            ExercisesRepository = exercisesRepository;
            RoutinesRepository = routinesRepository;
            WorkoutPlansRepository = workoutPlansRepository;
            WorkoutSetsRepository = workoutSetsRepository;
            SetsRepository = setsRepository;
            UsersRepository = usersRepository;
            CompletedRoutinesRepository = completedRoutinesRepository;
        }


        public IExercisesRepository ExercisesRepository { get; private set; }

        public IRoutinesRepository RoutinesRepository { get; private set; }

        public IWorkoutPlansRepository WorkoutPlansRepository { get; private set; }

        public IWorkoutSetsRepository WorkoutSetsRepository { get; private set; }

        public ISetsRepository SetsRepository { get; private set; }

        public IUserRepository UsersRepository { get; private set; }

        public ICompletedRoutinesRepository CompletedRoutinesRepository { get; private set; }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }


    }
}
