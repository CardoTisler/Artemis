using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
}