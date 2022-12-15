using System;
using DataModel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Infrastructure.Database
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext() { }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            // UserSkill
            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(skill => skill.UserSkills)
                .HasForeignKey(us => us.SkillId);

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(skill => skill.UserSkills)
                .HasForeignKey(us => us.UserId);

            // Skill
            modelBuilder.Entity<Skill>()
                .HasIndex(x => x.Title).IsUnique();

            // Post
            modelBuilder.Entity<Post>()
                .HasOne(us => us.Owner)
                .WithMany(post => post.Posts)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Post>()
                .HasOne(c => c.Category)
                .WithMany(post => post.Posts)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}

