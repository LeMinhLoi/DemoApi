using Microsoft.EntityFrameworkCore;
using Project.WebApi.Models.Domain;

namespace Project.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            #region Seed Data
            var id1 = new Guid("d381075c-8c50-4151-826f-1593b0a5cfad");
            var id2 = new Guid("d381075c-8c50-4151-826f-1593b0a5cfae");
            var id3 = new Guid("d381075c-8c50-4151-826f-1593b0a5cfaf");

            modelBuilder.Entity<WalkDifficulty>().HasData(
                    new WalkDifficulty() { Id = id1, Code = "Medium" },
                    new WalkDifficulty() { Id = id2, Code = "Hard" },
                    new WalkDifficulty() { Id = id3, Code = "Easy" }
                );




            var id4 = new Guid("a381075c-8c50-4151-826f-1593b0a5cfad");
            var id5 = new Guid("a381075c-8c50-4151-826f-1593b0a5cfae");
            var id6 = new Guid("a381075c-8c50-4151-826f-1593b0a5cfaf");
            modelBuilder.Entity<Region>().HasData(
                    new Region() { Id = id4, Code = "ABC", Name = "name1", Area = 100, Lat= 2, Long = 100, Population = 1000},
                    new Region() { Id = id5, Code = "XYZ", Name = "name2", Area = 100, Lat = 2, Long = 100, Population = 1000 },
                    new Region() { Id = id6, Code = "123", Name = "name3", Area = 100, Lat = 2, Long = 100, Population = 1000 }
                );

            modelBuilder.Entity<Walk>().HasData(
                    new Walk() { Id = id1, Length=10, Name= "Marathon", RegionId = id4, WalkDifficultyId = id1},
                    new Walk() { Id = id2, Length = 10, Name = "hellothon", RegionId = id5, WalkDifficultyId = id2 },
                    new Walk() { Id = id3, Length = 10, Name = "hithon", RegionId = id6, WalkDifficultyId = id3 }
                );

            modelBuilder.Entity<User>().HasData(
                    new User() 
                    { 
                        Id = id1,
                        Email = "loi@1234.com",
                        Name = "le minh loi",
                        UserName = "leminhloi",
                        Password = "loi@1234",
                        Phone = "0965920330",
                    },
                    new User()
                    {
                        Id = id2,
                        Email = "nhat@1234.com",
                        Name = "huynh nhat",
                        UserName = "huynhnhat",
                        Password = "nhat@1234",
                        Phone = "0965920331",
                    }
                );

            modelBuilder.Entity<Role>().HasData(
                    new Role()
                    {
                        Id = id1,
                        Name = "admin",
                        Description = "admin"
                    },
                    new Role()
                    {
                        Id = id2,
                        Name = "employee",
                        Description = "employee"
                    }
                );

            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole()
                    {
                        Id = id1,
                        RoleId = id1,
                        UserId = id1
                    },
                    new UserRole()
                    {
                        Id = id2,
                        RoleId = id2,
                        UserId = id2
                    }
                );
            #endregion
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }

    }
}
