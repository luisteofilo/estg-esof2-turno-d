using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildPermissions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(p => p.PermissionId);
            entity.Property(p => p.PermissionId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(p => p.RolePermissions)
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionId);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => rp.PermissionId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.RoleId);
            entity.Property(r => r.RoleId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId);
        });
    }
    
    private void BuildInterviewFeedback(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(j => j.JobId);
            entity.Property(j => j.JobId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(j => j.InterviewFeedbacks)
                .WithOne(ifb => ifb.Job)
                .HasForeignKey(ifb => ifb.JobId);
        });

        modelBuilder.Entity<InterviewFeedback>(entity =>
        {
            entity.HasKey(ifb => ifb.InterviewFeedbackId);
            entity.Property(ifb => ifb.InterviewFeedbackId)
                .HasDefaultValueSql("gen_random_uuid()");
        });
    }
}
