using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BlogPost>blogPosts { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
