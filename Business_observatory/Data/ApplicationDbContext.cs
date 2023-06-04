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
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
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
		public DbSet<Business_observatory.Models.ApplicationRole>? ApplicationRole { get; set; }
		public DbSet<Business_observatory.Models.ApplicationUser>? ApplicationUser { get; set; }
	}
}