using Microsoft.EntityFrameworkCore;

namespace QatlantisAPI.models
{
    public class DnDContext : DbContext
    {
        public DnDContext(DbContextOptions<DnDContext> options) : base(options)
        {

        }

        public DbSet<Case> Cases => Set<Case>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Customer> Customers => Set<Customer>();
    }
}
