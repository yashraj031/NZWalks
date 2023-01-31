using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NZWalksAPI.Model.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base(options)
        {

          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                        .HasOne(x => x.Role)
                        .WithMany(y => y.UserRoles)
                        .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User_Role>()
                        .HasOne(x => x.User)
                        .WithMany(y => y.UsersRoles)
                        .HasForeignKey(x => x.UserId);
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficultys { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> user_Roles { get; set; }

    }
}
