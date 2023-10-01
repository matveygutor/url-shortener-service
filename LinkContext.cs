using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test
{
    public class LinkContext : DbContext
    {
        public DbSet<Link> Links { get; set; } = null!;
       
        public LinkContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Link>().HasData(
        //            new Link { Id = 1, LongURL = "string", ShortURL = "string", Token = "string", Date = DateTime.Now, Click = 0}
        //    );
        //}
    }
}
