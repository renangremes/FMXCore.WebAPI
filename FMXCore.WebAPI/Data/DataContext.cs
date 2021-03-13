using FMXCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FMXCore.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Finance> Finances { get; set; }
    }
}