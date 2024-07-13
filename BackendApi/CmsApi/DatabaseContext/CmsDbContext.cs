using CmsApi.Enums;
using CmsApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.DatabaseContext
{
    
        public class CmsDbContext : DbContext
        {
            public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options) { }

            public DbSet<Customer> Customers { get; set; }
            public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            SeedUsers(modelBuilder);
            }
            private void SeedUsers(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<User>().HasData
               (
                 new User() { UserId=1,UserName = "adminDanat", Password = "Cms@P@$$w0rd", userType = UserType.Admin, HashPassword ="", SaltPassword ="", CreatedBy =1, CreatedAt =DateTime.Now}
               );
            }
        }
}
