using Business_observatory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Business_observatory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Categoriesproject> Categoriesprojects { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Models.File> Files { get; set; }

        public virtual DbSet<Project> Projects { get; set; }
        public DbSet<Business_observatory.Models.Contact>? Contact { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(a => a.Projects)
                .HasForeignKey(p => p.AspNetUserId);
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(e=>e.Id)
                .HasMaxLength(127);
                entity.Property(e=>e.Name)
                .HasMaxLength(50);
                entity.Property(e=>e.NormalizedName)
                .HasMaxLength(50);
            });
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id)
                .HasMaxLength(127);
                entity.Property(e => e.UserName)
                .HasMaxLength(127);
                entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(127);
            });
        }
        public DbSet<Business_observatory.Models.ApplicationRole>? ApplicationRoles { get; set; }
        public DbSet<Business_observatory.Models.ApplicationUser>? ApplicationUsers { get; set; }

    }
}