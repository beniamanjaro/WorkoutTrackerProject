using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Infrastructure;
using WorkoutTracker.Infrastructure.Repositories;
using WorkoutTracker.Presentation;
using WorkoutTracker.Presentation.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();
builder.Services.AddScoped<IRoutinesRepository, RoutinesRepository>();
builder.Services.AddScoped<ISetsRepository, SetsRepository>();
builder.Services.AddScoped<IWorkoutPlansRepository, WorkoutPlansRepository>();
builder.Services.AddScoped<IWorkoutSetsRepository, WorkoutSetsRepository>();
builder.Services.AddScoped<ICompletedRoutinesRepository, CompletedRoutinesRepository>();
builder.Services.AddDbContext<WorkoutContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMediatR(typeof(GetAllWorkoutPlans));
builder.Services.AddAutoMapper(typeof(WorkoutPlanProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
