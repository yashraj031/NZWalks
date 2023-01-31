using NZWalksAPI.Model.Domain;

namespace NZWalksAPI.Repository
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
