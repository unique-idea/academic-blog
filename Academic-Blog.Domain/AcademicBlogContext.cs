using Academic_Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain
{
    public class AcademicBlogContext : DbContext
    {
        public AcademicBlogContext() 
        { 
        }
        public AcademicBlogContext(DbContextOptions<AcademicBlogContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AccountAwardMapping> AccountAwardMappings { get; set; }
        public DbSet<AccountFieldMapping> AccountFieldMappings { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<BannedInfor> BannedInfors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }    
        public DbSet<Field> Fields { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TrackingViewBlog> TrackingViewBlogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=AcademicBlogDB;TrustServerCertificate=True");
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:DefaultDB"] !;
            return strConn;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
                entity.HasKey(entity=> entity.Id);
                entity.HasIndex(entity => entity.UserName, "UX_Account_Username");
                entity.Property(entity => entity.Name).HasMaxLength(100);
                entity.Property(entity => entity.Password).HasMaxLength(100);
                entity.Property(entity => entity.Status).HasMaxLength(20);
                entity.HasOne(d => d.Role).WithMany(p => p.Accounts).HasForeignKey(d => d.RoleId).HasConstraintName("FK_ACCOUNT_ROLE");
            });
            modelBuilder.Entity<AccountAwardMapping>(entity =>
            {
                entity.ToTable("AccountAwardMapping");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.DateTime).HasDefaultValueSql("getutcdate()");
                entity.HasOne(d => d.Lecturer).WithMany(p => p.LecturerAwardMappings).HasConstraintName("FK_Lecturer_Award").HasForeignKey(x => x.LecturerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.Student).WithMany(p => p.StudentAwardMappings).HasConstraintName("FK_Student_Award").HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
                
            });
            modelBuilder.Entity<Field>(entity =>
            {
                entity.ToTable("Field");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Status).HasMaxLength(20);

            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Status).HasMaxLength(20);

            });
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Status).HasMaxLength(20);
                entity.Property(entity => entity.CreatedTime).HasDefaultValueSql("getutcdate()");
                entity.HasOne(d => d.Author).WithMany(p => p.AuthorBlogs).HasConstraintName("FK_Author_Blog").HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.Reviewer).WithMany(p => p.ReviewBlogs).HasConstraintName("FK_Reviewer_Blog").HasForeignKey(x => x.ReviewerId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.CreateTime).HasDefaultValueSql("getutcdate()");
                entity.HasOne(d => d.Blog).WithMany(P => P.Comments).HasForeignKey(x => x.BlogId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Name).HasMaxLength(20);
            });
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.DateTime).HasDefaultValueSql("getutcdate()");
                entity.HasOne(d => d.FromUser).WithMany(p => p.MyImpactsNotifications).HasConstraintName("FK_Impact_Notification");
                entity.HasOne(d => d.ForUser).WithMany(p => p.MyNotifications).HasConstraintName("FK_My_Notification");
            });
            modelBuilder.Entity<BannedInfor>(entity =>
            {
                entity.HasOne(a => a.Account).WithMany(d => d.BannedInfors).HasConstraintName("Fk_Account_BannedInfor");
            });
            modelBuilder.Entity<AccountFieldMapping>(entity =>
            {
                entity.HasOne(a => a.Account).WithOne(d => d.AccountFieldMapping).HasForeignKey<Account>(x => x.AccountFieldMappingId);
            });

            modelBuilder.Entity<TrackingViewBlog>(entity => entity.ToTable("TrackingViewBlog"));
        }
    }
}
