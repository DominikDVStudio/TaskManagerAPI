using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.CreateTask;
using TaskManagerApi.Infrastructure.Repositories;
using TaskManagerApi.Application.UseCases.UpdateTask;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Singleton for testing operations
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<CreateTaskUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();