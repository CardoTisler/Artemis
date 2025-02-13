using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtemisApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
}