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
            IRoutinesRepository routinesRepository, IWorkoutPlanRepository workoutPlansRepository,
            IWorkoutSetsRepository workoutSetsRepository, ISetsRepository setsRepository, IUserRepository usersRepository)
        {
            _dataContext = dataContext;
            ExercisesRepository = exercisesRepository;
            RoutinesRepository = routinesRepository;
            WorkoutPlansRepository = workoutPlansRepository;
            WorkoutSetsRepository = workoutSetsRepository;
            SetsRepository = setsRepository;
            UsersRepository = usersRepository;
        }


        public IExercisesRepository ExercisesRepository { get; private set; }

        public IRoutinesRepository RoutinesRepository { get; private set; }

        public IWorkoutPlanRepository WorkoutPlansRepository { get; private set; }

        public IWorkoutSetsRepository WorkoutSetsRepository { get; private set; }

        public ISetsRepository SetsRepository { get; private set; }

        public IUserRepository UsersRepository { get; private set; }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }


    }
}
