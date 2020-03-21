using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Contract> Contracts { get; set; }
    }
}
