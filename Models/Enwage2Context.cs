using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Enwage_API.Models;

public partial class Enwage2Context : DbContext
{
    public Enwage2Context()
    {
    }

    public Enwage2Context(DbContextOptions<Enwage2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeStatename> EmployeeStatenames { get; set; }

    public virtual DbSet<Fileattachment> Fileattachments { get; set; }

    public virtual DbSet<Statename> Statenames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Enwage2;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Changestate).HasColumnName("changestate");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Experienceenddate).HasColumnName("experienceenddate");
            entity.Property(e => e.Experiencestartdate).HasColumnName("experiencestartdate");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Hourlyrate).HasColumnName("hourlyrate");
            entity.Property(e => e.Ispresent).HasColumnName("ispresent");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.Client).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("employee_client_id_fkey");
        });

        modelBuilder.Entity<EmployeeStatename>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_statename_pkey");

            entity.ToTable("employee_statename");

            entity.HasIndex(e => new { e.EmployeeId, e.StatenameId }, "unique_employee_statename").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.StatenameId).HasColumnName("statename_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeStatenames)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade) // Enable cascade delete
                .HasConstraintName("employee_statename_employee_id_fkey");

            entity.HasOne(d => d.Statename).WithMany(p => p.EmployeeStatenames)
                .HasForeignKey(d => d.StatenameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_statename_statename_id_fkey");
        });

        modelBuilder.Entity<Fileattachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fileattachment_pkey");

            entity.ToTable("fileattachment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Contenttype)
                .HasColumnType("character varying")
                .HasColumnName("contenttype");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Filedata).HasColumnName("filedata");
            entity.Property(e => e.Filename)
                .HasColumnType("character varying")
                .HasColumnName("filename");
            entity.Property(e => e.Uploaddate).HasColumnName("uploaddate");

            entity.HasOne(d => d.Employee).WithMany(p => p.Fileattachments)
                .HasForeignKey(d => d.Employeeid)
                .HasConstraintName("fileattachment_employeeid_fkey");
        });

        modelBuilder.Entity<Statename>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statename_pkey");

            entity.ToTable("statename");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
