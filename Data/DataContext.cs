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
    }
}