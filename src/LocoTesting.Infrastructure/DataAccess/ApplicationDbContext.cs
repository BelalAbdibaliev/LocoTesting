using LocoTesting.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.AnswerOptions)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Test>()
            .HasMany(t => t.Questions)
            .WithOne(q => q.Test)
            .HasForeignKey(q => q.TestId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.UserProfile)
            .WithOne(u => u.AppUser)
            .HasForeignKey<UserProfile>(up => up.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
}