using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
                new () { Id = 1, Name = "All Categories", Introduction = "<p>With a variety of topics to discuss,<i><strong> feel free to contribute your articles to my website.</strong></i></span></p>", CreatedOn = new(2021, 8, 7) },
                    new () { Id = 2, Name = "ASP.NET Core", Introduction = "<p><strong>ASP.NET Core</strong> is the open-source version of ASP.NET, that runs on macOS, Linux, and Windows. ASP.NET Core was first released in 2016 and is a re-design of earlier Windows-only versions of ASP.NET.</p>", CreatedOn = new(2021, 8, 7) },
                    new () { Id = 3, Name = "Angular", Introduction = "<p><strong>Angular </strong>is a platform and framework for building single-page client applications using HTML and TypeScript.</p>", CreatedOn = new(2021, 8, 7) },
                    new () { Id = 4, Name = "SQL", Introduction = "<p><strong>SQL </strong>stands for Structured Query Language. SQL is a standard language designed for managing data in a relational database management system.&nbsp;</p>", CreatedOn = new(2021, 8, 7) },
                    new () { Id = 5, Name = "Blockchain", Introduction = "Blockchain is a system of recording information in a way that makes it difficult or impossible to change, hack, or cheat the system.", CreatedOn = new(2021, 8, 7) }
                );
            modelBuilder.Entity<Role>().HasData(
                new () { Id = new ("d1823e23-02e3-443c-b0dc-85e46c97b10e"), Name = "Admin", CreatedOn = new(2021, 8, 7) },
                new () { Id = new ("d4314259-9d36-4af7-b75a-77f24e15600a"), Name = "Moderator", CreatedOn = new(2021, 8, 7) },
                new () { Id = new ("924fecba-2c1b-451c-92cd-83b92d8af6c3"), Name = "Member", CreatedOn = new(2021, 8, 7) }
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
