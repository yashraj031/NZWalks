using Microsoft.IdentityModel.Tokens;

namespace NZWalksAPI.Repository
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateAsync(string username, string password); 
    }
}
