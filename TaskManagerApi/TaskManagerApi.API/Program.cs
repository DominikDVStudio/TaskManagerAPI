using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.CreateTask;
using TaskManagerApi.Application.UseCases.DeleteTask;
using TaskManagerApi.Infrastructure.Repositories;
using TaskManagerApi.Application.UseCases.UpdateTask;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Application.Queries;
using TaskManagerApi.Infrastructure.Data;
using TaskManagerApi.Middleware;
using TaskManagerApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTaskDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<CreateTaskUseCase>();
builder.Services.AddScoped<DeleteTaskUseCase>();
builder.Services.AddScoped<GetTasksQueryHandler>();
builder.Services.AddScoped<GetTaskByIdQueryHandler>();

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