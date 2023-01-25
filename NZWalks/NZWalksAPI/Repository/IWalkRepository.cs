using NZWalksAPI.Model.Domain;

namespace NZWalksAPI.Repository
{
    public interface IWalkRepository
    {
       Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> DeleteAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}
