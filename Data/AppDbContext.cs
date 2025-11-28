using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        //public DbSet<UserEntity> Users { get; set; }

        // OnModelCreating if needed:
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TaskEntity>()
        //        .HasOne(t => t.User)
        //        .WithMany(u => u.Tasks)
        //        .HasForeignKey(t => t.UserId);

        //    // existing configs...
        //}

    }
}
