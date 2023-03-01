using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week06_2_AsyncDatabaseAccess.Models;

namespace Week06_2_AsyncDatabaseAccess.Data
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    class AppDbContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=PersonDb;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


        public DbSet<Person> Persons { get; set; }
    }

}
