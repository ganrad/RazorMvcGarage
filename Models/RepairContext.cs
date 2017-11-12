using Microsoft.EntityFrameworkCore;

namespace RazorMvcGarage.Models
{
    public class RepairContext : DbContext
    {
        public RepairContext(DbContextOptions<RepairContext> options)
                : base(options)
        {
        }

        public DbSet<Repair> Repair { get; set; }
    }
}