using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Model
{
    public partial class ERMContext : DbContext
    {
        public ERMContext()
        {
        }

        public ERMContext(DbContextOptions<ERMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CClientCompany> CClientCompanies { get; set; }
        public virtual DbSet<CEmployee> CEmployees { get; set; }
        public virtual DbSet<CErpsystem> CErpsystems { get; set; }
        public virtual DbSet<CIssue> CIssues { get; set; }
        public virtual DbSet<CMeeting> CMeetings { get; set; }
        public virtual DbSet<CPosition> CPositions { get; set; }
        public virtual DbSet<VEmployeeAtMeeting> VEmployeeAtMeetings { get; set; }
        public virtual DbSet<VTask> VTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<CClientCompany>(entity =>
            {
                entity.ToTable("cClientCompany");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CEmployee>(entity =>
            {
                entity.ToTable("cEmployee");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Patronymic).HasMaxLength(20);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.CEmployees)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__cEmployee__Posit__2E1BDC42");
            });

            modelBuilder.Entity<CErpsystem>(entity =>
            {
                entity.ToTable("cERPSystem");

                entity.Property(e => e.CurrentVersion).HasMaxLength(8);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.CErpsystems)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__cERPSyste__Clien__267ABA7A");
            });

            modelBuilder.Entity<CIssue>(entity =>
            {
                entity.ToTable("cIssue");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasOne(d => d.System)
                    .WithMany(p => p.CIssues)
                    .HasForeignKey(d => d.SystemId)
                    .HasConstraintName("FK__cIssue__SystemId__29572725");
            });

            modelBuilder.Entity<CMeeting>(entity =>
            {
                entity.ToTable("cMeeting");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Topic).HasMaxLength(50);
            });

            modelBuilder.Entity<CPosition>(entity =>
            {
                entity.ToTable("cPosition");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<VEmployeeAtMeeting>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.MeetingId })
                    .HasName("PK_EmployeeAtMeetingPKConstraint");

                entity.ToTable("vEmployeeAtMeeting");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.VEmployeeAtMeetings)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__vEmployee__Emplo__35BCFE0A");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.VEmployeeAtMeetings)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__vEmployee__Meeti__36B12243");
            });

            modelBuilder.Entity<VTask>(entity =>
            {
                entity.ToTable("vTask");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.VTasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__vTask__EmployeeI__31EC6D26");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.VTasks)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("FK__vTask__IssueId__30F848ED");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
