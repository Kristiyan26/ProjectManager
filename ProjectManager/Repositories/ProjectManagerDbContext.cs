using Microsoft.EntityFrameworkCore;
using ProjectManager.Entities;

namespace ProjectManager.Repositories
{
    public class ProjectManagerDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<UserToProject> UserToProjects { get; set; } 
        public ProjectManagerDbContext()
        {
            this.Users = this.Set<User>();
            this.Projects = this.Set<Project>();
            this.UserToProjects = this.Set<UserToProject>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=ProjectManagerDB;Trusted_Connection=True;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "MladMilioner",
                    Password = "2604",
                    FirstName = "Kristiyan",
                    LastName = "Lyubenov"
                }

                );
        }
    }
}
