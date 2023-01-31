using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Model.Domain;

namespace NZWalksAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password); 
    }
}
