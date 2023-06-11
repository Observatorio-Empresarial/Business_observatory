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

        public virtual DbSet<Archivo> Archivos { get; set; }
		public virtual DbSet<Categoria> Categorias { get; set; }
		public virtual DbSet<Contacto> Contactos { get; set; }
		public virtual DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

		public DbSet<IdentityRole> IdentityRoles { get; set; }
		public DbSet<IdentityUser> IdentityUsers { get; set; }
		public DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            // Each User can have many entries in the UserRole join table
            modelBuilder.Entity<ApplicationUser>(b => { 
			b.HasMany(e=>e.UserRoles)
				.WithOne(e=>e.User)
				.HasForeignKey(ur=>ur.UserId)
				.IsRequired();
			});
            // Each Role can have many entries in the UserRole join table
            modelBuilder.Entity<ApplicationRole>(b =>
			{
				b.HasMany(e=>e.UserRoles)
				.WithOne(e=>e.Role)
				.HasForeignKey(ur=>ur.RoleId)
				.IsRequired();
			});
			//Relacion uno entre contacto y ApplicationUser

			modelBuilder.Entity<Contacto>()
				.HasOne(c => c.ApplicationUser)
				.WithOne(a => a.Contacto)
				.HasForeignKey<Contacto>(c => c.AspNetUserId);

			modelBuilder.Entity<Proyecto>()
				.HasOne(p => p.ApplicationUser)
				.WithMany(a => a.Proyectos)
				.HasForeignKey(p => p.AspNetUserId);
			modelBuilder.Entity<ApplicationRole>(entity =>
			{
				entity.Property(e => e.Id)
				.HasMaxLength(127);
				entity.Property(e => e.Name)
				.HasMaxLength(50);
				entity.Property(e => e.NormalizedName)
				.HasMaxLength(50);
			});
			modelBuilder.Entity<Models.ApplicationUser>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Models.ApplicationUser>>)(entity =>
			{
				entity.Property<string>(e => (string)e.Id)
				.HasMaxLength(127);
				entity.Property<string>(e => (string)e.UserName)
				.HasMaxLength(127);
				entity.Property<string>(e => (string)e.NormalizedUserName)
				.HasMaxLength(127);
			}));
		}

    }
}