using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Emails;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using ESOF.WebApp.DBLayer.Entities.Interviews;

using ESOF.WebApp.DBLayer.Entities.Emails;

using ESOF.WebApp.DBLayer.Persistence;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Job = ESOF.WebApp.DBLayer.Entities.Job;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext : DbContext, IUnitOfWork
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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> _)
        : base(DefaultOptions)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    
    public DbSet<Client> Clients { get; set; } // DbSet para armazenar clientes
    public DbSet<Talent> Talents { get; set; } // DbSet para armazenar talentos
    
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
    // Email Template
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    // Job Features
    public DbSet<Job> Jobs { get; set; }

    public DbSet<Import> Imports{ get; set; }

    public DbSet<JobSkill> JobSkills { get; set; }
    
    // FAQ Features
    public DbSet<Question> FAQQuestions { get; set; }
    public DbSet<Answer> FAQAnswers { get; set; }
    
    public DbSet<Entities.FAQ.Job> FAQJobs { get; set; }


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

        // Job Features
        BuildJobs(modelBuilder);
        BuildImports(modelBuilder);
        BuildJobSkills(modelBuilder);

        // Interview Features 
        BuildInterviews(modelBuilder);
        BuildInterviewer(modelBuilder);
        BuildCandidates(modelBuilder);

        
        // FAQ Features
        BuildFAQJobs(modelBuilder);
        BuildFAQQuestions(modelBuilder);
        BuildFAQAnswers(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
