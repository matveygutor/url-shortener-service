using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test
{
    public class LinkContext : DbContext
    {
        public LinkContext()
        {
        }

        public LinkContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Link> Links { get; set; } = null!;
    }
}
