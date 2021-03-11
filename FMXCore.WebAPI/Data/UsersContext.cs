using FMXCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMXCore.WebAPI.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Finance> Finances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)        {

            string databaseFile = Environment.CurrentDirectory + @"\Resources\DataBase\FMX.db";
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Data Source={databaseFile};");
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}
