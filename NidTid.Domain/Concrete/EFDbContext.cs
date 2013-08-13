using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete
{
    public class EFDbContext : DbContext {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
