using Microsoft.EntityFrameworkCore;

namespace LinqSpecs.DatabaseTests
{
    class SampleDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=file::memory:?cache=shared");
        }
    }
}
