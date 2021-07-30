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
            modelBuilder.Entity<Category>().ToTable(nameof(Category));
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "All Categories", Introduction = "<p>With a variety of topics to discuss,<i><strong> feel free to contribute your articles to my website.</strong></i></span></p>", CreatedOn = System.DateTime.Now },
                    new Category() { Id = 2, Name = "ASP.NET Core", Introduction = "<p><strong>ASP.NET Core</strong> is the open-source version of ASP.NET, that runs on macOS, Linux, and Windows. ASP.NET Core was first released in 2016 and is a re-design of earlier Windows-only versions of ASP.NET.</p>", CreatedOn = System.DateTime.Now },
                    new Category() { Id = 3, Name = "Angular", Introduction = "<p><strong>Angular </strong>is a platform and framework for building single-page client applications using HTML and TypeScript.</p>", CreatedOn = System.DateTime.Now },
                    new Category() { Id = 4, Name = "SQL", Introduction = "<p><strong>SQL </strong>stands for Structured Query Language. SQL is a standard language designed for managing data in a relational database management system.&nbsp;</p>", CreatedOn = System.DateTime.Now },
                    new Category() { Id = 5, Name = "Blockchain", Introduction = "Blockchain is a system of recording information in a way that makes it difficult or impossible to change, hack, or cheat the system.", CreatedOn = System.DateTime.Now }
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
