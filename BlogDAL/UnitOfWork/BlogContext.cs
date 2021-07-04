using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogDAL.UnitOfWork
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<Role>().ToTable(nameof(Role));
            modelBuilder.Entity<Article>().ToTable(nameof(Article));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
