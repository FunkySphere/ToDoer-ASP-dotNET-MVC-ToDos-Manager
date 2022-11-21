using Microsoft.EntityFrameworkCore;
using ToDoer.Models;

namespace ToDoer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<ToDo>? ToDos { get; set; }
    public DbSet<ArchivedToDo> ArchivedTasks { get; set; }
}