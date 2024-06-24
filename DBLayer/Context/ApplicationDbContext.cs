using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Emails;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext : DbContext
{
    private static readonly DbContextOptions DefaultOptions = new Func<DbContextOptions>(() =>
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        var db = EnvFileHelper.GetString("POSTGRES_DB");
        var user = EnvFileHelper.GetString("POSTGRES_USER");
        var password = EnvFileHelper.GetString("POSTGRES_PASSWORD");
        var port = EnvFileHelper.GetString("POSTGRES_PORT");
        var host = EnvFileHelper.GetString("POSTGRES_HOST");

        if (string.IsNullOrEmpty(db) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(port) || string.IsNullOrEmpty(host))
        {
            throw new InvalidOperationException(
                "Database connection information not fully specified in environment variables.");
        }

        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";
        optionsBuilder.UseNpgsql(connectionString);
        return optionsBuilder.Options;
    })();
    
    public ApplicationDbContext()
        : base(DefaultOptions)
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    
    public DbSet<Client> Clients { get; set; } // DbSet para armazenar clientes
    public DbSet<Talent> Talents { get; set; } // DbSet para armazenar talentos
    
    public DbSet<EmailTemplate> EmailTemplates { get; set; } // DbSet para armazenar templates de email
    
    // Profile Features
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<ProfileSkill> ProfileSkills { get; set; }
    public DbSet<Skill> Skills { get; set; }
    
    // Interview Features
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<Interviewer> Interviewers { get; set; }
    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildUsers(modelBuilder);
        BuildRoles(modelBuilder);
        BuildPermissions(modelBuilder);
        BuildRolePermissions(modelBuilder);
        BuildUserRoles(modelBuilder);
        BuildClients(modelBuilder);
        BuildTalents(modelBuilder);
        
        // Profile Features 
        BuildProfiles(modelBuilder);
        BuildExperiences(modelBuilder);
        BuildEducations(modelBuilder);
        BuildProfileSkills(modelBuilder);
        BuildSkills(modelBuilder);
        
        // Interview Features 
        BuildInterviews(modelBuilder);
        BuildInterviewer(modelBuilder);
        BuildCandidates(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
}
