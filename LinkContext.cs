using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test
{
    public class LinkContext : DbContext
    {
        public DbSet<Link> Links { get; set; } = null!;
       
        public LinkContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
