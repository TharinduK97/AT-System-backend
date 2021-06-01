using hp_proj_1_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace hp_proj_1_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
         public DbSet<Job> Jobs { get; set; }
         public DbSet<User> Users { get; set; }
         public DbSet<AppliedJob> AppliedJobs { get; set; }


          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Applicant");
        }
    }
}