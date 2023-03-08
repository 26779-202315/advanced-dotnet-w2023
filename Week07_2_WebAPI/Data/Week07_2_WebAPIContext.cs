using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Week07_2_WebAPI.Model;

namespace Week07_2_WebAPI.Data
{
    public class Week07_2_WebAPIContext : DbContext
    {
        public Week07_2_WebAPIContext (DbContextOptions<Week07_2_WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Week07_2_WebAPI.Model.Person> Person { get; set; } = default!;
    }
}
