using Common.Models;
using Common.Models.Lookups;
using Common.Models.UserManagement;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Api.Context
{
    public class MvpsDashboardDbContext : DbContext
    {
        public MvpsDashboardDbContext(DbContextOptions<MvpsDashboardDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserManagement Classes
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("UserId");
                entity.Property(m => m.Email).HasColumnName("EmailAddress");
            });
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("UserRoleId");
            });

            modelBuilder.Entity<UserRoleAssignment>(entity =>
            {
                entity.ToTable("UserRoleAssignment");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("UserRoleAssignmentId");
                entity.Property(m => m.ConditionId).HasColumnName("EventCodeId");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.ToTable("UserSetting");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");
            });
            #endregion

            #region Lookups
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("DimProfiles");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("ProfileId");
                entity.Property(m => m.Name).HasColumnName("ProfileName");

            });
            modelBuilder.Entity<Condition>(entity =>
            {
                entity.ToTable("DimEvents");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("EventId");
                entity.Property(m => m.Code).HasColumnName("EventCode");
                entity.Property(m => m.Name).HasColumnName("EventDescription");
                entity.Property(m => m.ProfileId).HasColumnName("MsgProfileId");
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("RoleId");
                // If you need to configure a foreign key, do it here using entity.HasOne/HasMany
                // Example (uncomment and adjust as needed):
                // entity.HasOne<Role>().WithMany().HasForeignKey(r => r.user);
            });
            modelBuilder.Entity<Jurisdiction>(entity =>
            {
                entity.ToTable("DimJurisdictions");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("JurisdictionId");
                entity.Property(m => m.Code).HasColumnName("JurisdictionCode");
                entity.Property(m => m.Abbreviation).HasColumnName("JurisdictionAltCode");
                entity.Property(m => m.Description).HasColumnName("JurisdictionDescription");
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("DimCategory");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("CategoryId");
                entity.Property(m => m.Description).HasColumnName("CategoryDescription");
            });
            modelBuilder.Entity<ProgramDto>(entity =>
            {
                entity.ToTable("DimPrograms");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("ProgramId");
                entity.Property(m => m.Name).HasColumnName("ProgramName");
            });
            #endregion
        }
    }
}