using COMP2139_Labs.Models;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    
    public DbSet<ProjectComment> ProjectComments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Define One-to-Many Relationship
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)                   // One project has (potentially) many Tasks
            .WithOne(t => t.Project)             // Each ProjectTask belongs to one project
            .HasForeignKey(t => t.ProjectId)    // Foreign Key in ProjectTask table
            .OnDelete(DeleteBehavior.Cascade);          // Cascade delete ProjectTask when Project is deleted
        
        // Seed the database with two initial projects
        modelBuilder.Entity<Project>().HasData(
            new Project { ProjectId = 1, ProjectName = "Assignment 1", ProjectDescription = "COMP2139 Assignment 1" },
            new Project { ProjectId = 2, ProjectName = "Assignment 2", ProjectDescription = "COMP2139 Assignment 2" }
        );

    }
}