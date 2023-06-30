using Microsoft.EntityFrameworkCore;

namespace BigBangAPI.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> doctors { get; set; }

        public DbSet<Patients> patients { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
