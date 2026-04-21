using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}