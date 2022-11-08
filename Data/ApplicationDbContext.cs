using Microsoft.EntityFrameworkCore;
using ToDoer.Models;

namespace ToDoer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}