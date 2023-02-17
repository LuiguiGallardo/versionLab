using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users {get; set;}
        public DbSet<Project> Project {get; set;}
        public DbSet<UserProject> UserProject {get; set;}
        public DbSet<Document> Document {get; set;}
        public DbSet<ProjectDocument> ProjectDocument {get; set;}

    }
}