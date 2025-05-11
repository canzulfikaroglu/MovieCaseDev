using Microsoft.EntityFrameworkCore;
using MovieCaseDev.Entities;
namespace MovieCaseDev.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
    }
}
