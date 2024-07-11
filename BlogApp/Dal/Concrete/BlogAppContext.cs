using System;
using System.Collections.Generic;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogApp.Dal.Concrete
{
    public partial class BlogAppContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BlogAppContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BlogAppContext(DbContextOptions<BlogAppContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA5E49E368");

                entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__PostId__3C69FB99");

                entity.HasOne(d => d.User).WithMany(p => p.Comments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__UserId__3D5E1FD2");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId).HasName("PK__Posts__AA12601808B0128C");

                entity.HasOne(d => d.User).WithMany(p => p.Posts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Posts__UserId__398D8EEE");

                entity.HasMany(d => d.Tag).WithMany(p => p.Post)
                    .UsingEntity<Dictionary<string, object>>(
                        "PostTags",
                        r => r.HasOne<Tag>().WithMany()
                            .HasForeignKey("TagId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__PostTags__TagId__4316F928"),
                        l => l.HasOne<Post>().WithMany()
                            .HasForeignKey("PostId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__PostTags__PostId__4222D4EF"),
                        j =>
                        {
                            j.HasKey("PostId", "TagId").HasName("PK__PostTags__7C45AF82DA1E5CDE");
                        });
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagId).HasName("PK__Tags__657CF9AC06ACC2EC");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C52F1D967");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
