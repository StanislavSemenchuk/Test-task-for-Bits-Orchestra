using Microsoft.EntityFrameworkCore;

namespace TestApplication.Models
{
    public class MyDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CsvFile> CsvFiles { get; set; }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
