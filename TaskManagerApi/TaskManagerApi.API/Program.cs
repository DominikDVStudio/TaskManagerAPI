using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.CreateTask;
using TaskManagerApi.Application.UseCases.DeleteTask;
using TaskManagerApi.Infrastructure.Repositories;
using TaskManagerApi.Application.UseCases.UpdateTask;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<CreateTaskUseCase>();
builder.Services.AddScoped<DeleteTaskUseCase>();

// Db registration 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();