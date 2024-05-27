using HashLibrary;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;


namespace SvelteApp1.Server.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<Video> Videos { get; set; } = null!;
        public DbSet<Favourite> Favourites { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}

