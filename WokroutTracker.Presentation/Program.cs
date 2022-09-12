using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
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

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<WorkoutContext>();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});


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

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }