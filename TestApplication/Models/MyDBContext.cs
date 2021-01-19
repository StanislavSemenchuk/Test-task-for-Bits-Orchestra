using Microsoft.EntityFrameworkCore;

namespace TestApplication.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
    }
}
