using Microsoft.EntityFrameworkCore;




namespace YourNamespace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Role> Role { get; set; }
        public DbSet<Grade> Grade { get; set; }


    }
}
