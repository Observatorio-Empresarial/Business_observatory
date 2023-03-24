using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Business_observatory.Models;

public partial class ObservatorioEmpresarialContext : DbContext
{
    public ObservatorioEmpresarialContext()
    {
    }

    public ObservatorioEmpresarialContext(DbContextOptions<ObservatorioEmpresarialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<Enterprise> Enterprises { get; set; }

    public virtual DbSet<Incharge> Incharges { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=observatorio_empresarial;user=root;password=", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Download>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("downloads");

            entity.HasIndex(e => e.IdProject, "id_project");

            entity.HasIndex(e => e.IdUser, "id_user");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DownloadDate).HasColumnName("download_date");
            entity.Property(e => e.IdProject)
                .HasColumnType("int(11)")
                .HasColumnName("id_project");
            entity.Property(e => e.IdUser)
                .HasColumnType("int(11)")
                .HasColumnName("id_user");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("downloads_ibfk_2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("downloads_ibfk_1");
        });

        modelBuilder.Entity<Enterprise>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("enterprises");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_user");
            entity.Property(e => e.Item)
                .HasMaxLength(20)
                .HasColumnName("item");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .HasColumnName("nit");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Enterprise)
                .HasForeignKey<Enterprise>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enterprises_ibfk_1");
        });

        modelBuilder.Entity<Incharge>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("incharges");

            entity.HasIndex(e => e.IdEnterprise, "id_enterprise");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_user");
            entity.Property(e => e.IdEnterprise)
                .HasColumnType("int(11)")
                .HasColumnName("id_enterprise");

            entity.HasOne(d => d.IdEnterpriseNavigation).WithMany(p => p.Incharges)
                .HasForeignKey(d => d.IdEnterprise)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incharges_ibfk_2");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Incharge)
                .HasForeignKey<Incharge>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incharges_ibfk_1");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("managers");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Manager)
                .HasForeignKey<Manager>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("managers_ibfk_1");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PRIMARY");

            entity.ToTable("projects");

            entity.Property(e => e.IdProject)
                .HasColumnType("int(11)")
                .HasColumnName("id_project");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .HasColumnName("file");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("'1'")
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("students");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_user");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .HasColumnName("student_code");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("students_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.AspNetUserId, "FK_AspNetUsers_Users");

            entity.Property(e => e.IdUser)
                .HasColumnType("int(11)")
                .HasColumnName("id_user");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("creation_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(85)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.SecondLastName)
                .HasMaxLength(60)
                .HasColumnName("second_last_name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("'1'")
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.TypeUser)
                .HasColumnType("enum('empresa','encargado','estudiante')")
                .HasColumnName("type_user");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .HasColumnName("update_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
