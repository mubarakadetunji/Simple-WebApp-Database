using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Scheduler.Models;

namespace Scheduler.Data
{
    public partial class SchedulerContext : DbContext
    {
        public SchedulerContext()
        {
        }

        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concentration> Concentrations { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Coursefeedback> Coursefeedbacks { get; set; } = null!;
        public virtual DbSet<Professor> Professors { get; set; } = null!;
        public virtual DbSet<Professorfeedback> Professorfeedbacks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Concentration>(entity =>
            {
                entity.HasKey(e => e.ConcentrationName)
                    .HasName("PRIMARY");

                entity.ToTable("concentrations");

                entity.Property(e => e.ConcentrationName).HasMaxLength(100);

                entity.Property(e => e.Notes).HasMaxLength(300);

                entity.Property(e => e.Requirements).HasMaxLength(1000);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(10)
                    .HasColumnName("CourseID");

                entity.Property(e => e.Antirequisites).HasMaxLength(200);

                entity.Property(e => e.CourseName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Notes).HasMaxLength(300);

                entity.Property(e => e.Prerequisites).HasMaxLength(200);

                entity.Property(e => e.Rating).HasColumnType("int(11)");

                entity.Property(e => e.Units).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Coursefeedback>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.Username })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("coursefeedback");

                entity.HasIndex(e => e.Username, "Username");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(10)
                    .HasColumnName("CourseID");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Qualities).HasMaxLength(100);

                entity.Property(e => e.Rating).HasColumnType("int(11)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Coursefeedbacks)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("coursefeedback_ibfk_1");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Coursefeedbacks)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("coursefeedback_ibfk_2");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.ProfessorName)
                    .HasName("PRIMARY");

                entity.ToTable("professors");

                entity.Property(e => e.ProfessorName).HasMaxLength(50);

                entity.Property(e => e.Rating).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Professorfeedback>(entity =>
            {
                entity.HasKey(e => new { e.ProfessorName, e.Username })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("professorfeedback");

                entity.HasIndex(e => e.Username, "Username");

                entity.Property(e => e.ProfessorName).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Qualities).HasMaxLength(100);

                entity.Property(e => e.Rating).HasColumnType("int(11)");

                entity.HasOne(d => d.ProfessorNameNavigation)
                    .WithMany(p => p.Professorfeedbacks)
                    .HasForeignKey(d => d.ProfessorName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("professorfeedback_ibfk_1");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Professorfeedbacks)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("professorfeedback_ibfk_2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.CoursesTaken).HasMaxLength(100);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Funds).HasColumnType("int(11)");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.SchoolEmail).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
