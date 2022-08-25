// See https://aka.ms/new-console-template for more information
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using WorkoutTracker.Application.Exercises.Queries;
using WorkoutTracker.Application.WorkoutPlans.Commands;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Infrastructure;
using WorkoutTracker.Infrastructure.Repositories;


var result = await test.GetRoutineById(1);

var diContainer = new ServiceCollection()
    .AddScoped<IExercisesRepository, ExercisesRepository>()
    .AddScoped<IWorkoutPlanRepository, WorkoutPlansRepository>()
    .AddScoped<IRoutinesRepository, RoutinesRepository>()
    .AddScoped<IWorkoutSetsRepository, WorkoutSetsRepository>()
    .AddScoped<ISetsRepository, SetsRepository>()
    .AddScoped<IUserRepository, UsersRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddMediatR(typeof(GetAllExercises).Assembly)
    .AddDbContext<WorkoutContext>(
                options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WorkoutsSampleDb;"))
    .BuildServiceProvider();


var mediator = diContainer.GetRequiredService<IMediator>();
var exercises = await mediator.Send(new GetAllExercises());



var routines = new[] { new Routine() { Name = "test", DayOrderNumber = 2, WorkoutSets = new[] { new WorkoutSet() { Sets = new[] { new Set() { ExerciseId = 1, NumberOfReps = 10, Weight = 20 }, } }, } }, };

Console.WriteLine();
var planToAddCommand = new CreateWorkoutPlan();
var planToAdd = new { UserId = 1, Name = "Workout Plan Name Test", TimesPerWeek = 3, Routines = routines };
planToAddCommand.Name = planToAdd.Name;
planToAddCommand.Routines = planToAdd.Routines;
planToAddCommand.UserId = planToAdd.UserId;
planToAddCommand.TimesPerWeek = planToAdd.TimesPerWeek;
var y = await mediator.Send(planToAddCommand);


var getWorkoutPlansQuery = new GetWorkoutPlansByUser();
getWorkoutPlansQuery.UserId = 1;
var z = await mediator.Send(getWorkoutPlansQuery);

foreach(var x in z)
{
    Console.WriteLine(x.Name);
}

await mediator.Send(new UpdateWorkoutPlan {Id = 2, Name = "Changed Name" });
await mediator.Send(new DeleteWorkoutPlan {Id = 2 });


Console.ReadKey();
