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
            #endregion
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
    }
}
