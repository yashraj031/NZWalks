using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Model.Domain;
using NZWalksAPI.Model.DTO;

namespace NZWalksAPI.Repository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Model.Domain.WalkDifficulty> AddAsync(Model.Domain.WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<Model.Domain.WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficultys.FindAsync(id);
            if (existingWalkDifficulty == null) 
            {
                return null;
            }

            nZWalksDbContext.WalkDifficultys.Remove(existingWalkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }

        public async Task<IEnumerable<Model.Domain.WalkDifficulty>> GetAllAsync()
        {
           return await nZWalksDbContext.WalkDifficultys.ToListAsync();
        }

        public async Task<Model.Domain.WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Model.Domain.WalkDifficulty> UpdateAsync(Guid id, Model.Domain.WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficultys.FindAsync(id);
            if (existingWalkDifficulty != null)
            {
                existingWalkDifficulty.Code = walkDifficulty.Code;
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalkDifficulty;
            }
            return null;
        }
    }
}
