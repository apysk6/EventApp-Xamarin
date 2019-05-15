using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventAppApi.Models;

namespace EventAppApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<EventAppApi.Models.Event> Event { get; set; }
    }
}
