using Microsoft.EntityFrameworkCore;
using ToDoData.Models;

namespace ToDoData.DataAccess;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ToDoModel> ToDos { get; set; }
    public DbSet<UserModel> Users { get; set; }
}