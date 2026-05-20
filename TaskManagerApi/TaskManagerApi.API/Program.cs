using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Application.Queries;
using TaskManagerApi.Application.Queries.TaskItems;
using TaskManagerApi.Application.UseCases.TaskItems.CreateTask;
using TaskManagerApi.Application.UseCases.TaskItems.DeleteTask;
using TaskManagerApi.Application.UseCases.TaskItems.UpdateTask;
using TaskManagerApi.Application.UseCases.Users.RegisterUser;
using TaskManagerApi.Infrastructure;
using TaskManagerApi.Infrastructure.Auth;
using TaskManagerApi.Infrastructure.Data;
using TaskManagerApi.Middleware;
using TaskManagerApi.Validators;
using TaskManagerApi.Validators.TaskItems;
using TaskManagerApi.Validators.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<CreateTaskUseCase>();
builder.Services.AddScoped<DeleteTaskUseCase>();

builder.Services.AddScoped<GetTasksQueryHandler>();
builder.Services.AddScoped<GetTaskByIdQueryHandler>();

builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();


// Db registration 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();