// See https://aka.ms/new-console-template for more information
using ConsoleApp.EfCoreModeling;
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



var diContainer = new ServiceCollection()
    .AddScoped<IExercisesRepository, ExercisesRepository>()
    .AddScoped<IWorkoutPlansRepository, WorkoutPlansRepository>()
    .AddScoped<IRoutinesRepository, RoutinesRepository>()
    .AddScoped<IWorkoutSetsRepository, WorkoutSetsRepository>()
    .AddScoped<ISetsRepository, SetsRepository>()
    .AddScoped<IUserRepository, UsersRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddMediatR(typeof(GetAllExercises).Assembly)
    .AddDbContext<WorkoutContext>(
                options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WorkoutsSampleDb;"))
    .BuildServiceProvider();


await Seeder.SeedData();
await Queries.RunQueries();
Console.ReadKey();
