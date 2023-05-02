using EmpactProject.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EmpactProject.Model
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<News> News { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<News>()
        .HasMany(e => e.Categories)
        .WithMany(e => e.News);


         modelBuilder.Entity<News>()
        .HasOne(e => e.Publisher)
        .WithMany(e => e.News)
        .HasForeignKey(e => e.PublisherId)
        .IsRequired();
        }
      
    }
}
