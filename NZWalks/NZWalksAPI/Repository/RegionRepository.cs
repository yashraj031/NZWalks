﻿using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Model.Domain;

namespace NZWalksAPI.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
           region.Id = Guid.NewGuid();
           await nZWalksDbContext.Regions.AddAsync(region);
           await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region =await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
          return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var existingRegion = await nZWalksDbContext.Regions.FindAsync(id);
            if (existingRegion == null) 
            {
                return null;
            }
            
            existingRegion.Name= region.Name;
            existingRegion.last= region.last;
            existingRegion.Long= region.Long;
            existingRegion.Area= region.Area;
            existingRegion.Population= region.Population;
            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
 