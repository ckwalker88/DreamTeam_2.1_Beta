using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BucBoard.Models.Entities.Existing
{
    public partial class BucBoardDBContext : DbContext
    {
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<DayOfTheWeek> DayOfTheWeek { get; set; }
        public virtual DbSet<ProfilePicture> ProfilePicture { get; set; }
        public virtual DbSet<Time> Time { get; set; }

        public BucBoardDBContext(DbContextOptions<BucBoardDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.Property(e => e.ApplicationUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Announcement)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("FK_ApplicationUserId_AnnouncementID");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasIndex(e => e.RolesId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.RolesId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.ApplicationUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.CourseName).IsRequired();

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("FK_Course");
            });

            modelBuilder.Entity<DayOfTheWeek>(entity =>
            {
                entity.Property(e => e.ApplicationUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DayOfTheWeek1)
                    .IsRequired()
                    .HasColumnName("DayOfTheWeek");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.DayOfTheWeek)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("FK_Day");
            });

            modelBuilder.Entity<ProfilePicture>(entity =>
            {
                entity.Property(e => e.ApplicationUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.ProfilePictureNavigation)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("FK_ApplicationUserId_AppUserID");
            });

            modelBuilder.Entity<Time>(entity =>
            {
                entity.Property(e => e.ApplicationUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.StartTime).IsRequired();

                entity.Property(e => e.StopTime).IsRequired();

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Time)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("FK_Time");
            });
        }
    }
}
