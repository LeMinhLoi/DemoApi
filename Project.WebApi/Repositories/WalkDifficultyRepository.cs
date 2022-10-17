using Microsoft.EntityFrameworkCore;
using Project.WebApi.Data;
using Project.WebApi.Models.Domain;

namespace Project.WebApi.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetAsync(Guid id);

        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);

        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty);
    }
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly ApplicationDbContext _context;
        public WalkDifficultyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _context.AddAsync(walkDifficulty);
            await _context.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _context.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDifficulty == null)
            {
                return null;
            }

            // Delete the region
            _context.WalkDifficulties.Remove(walkDifficulty);
            await _context.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _context.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await _context.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existing = await _context.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
            {
                return null;
            }

            existing.Code = walkDifficulty.Code;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
