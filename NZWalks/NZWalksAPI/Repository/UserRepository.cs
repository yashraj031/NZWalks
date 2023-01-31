using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Migrations;
using NZWalksAPI.Model.Domain;
using System.Linq;

namespace NZWalksAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public UserRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await nZWalksDbContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username && x.Password == password);

            if (user == null) 
            {
                return null;
            }
            var userRoles = await nZWalksDbContext.user_Roles.Where(
                x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles) 
                {
                    var role = await nZWalksDbContext.Roles.FirstOrDefaultAsync(
                        x => x.Id == userRole.RoleId);
                    if (role != null) 
                    {
                        user.Roles.Add(role.Name);
                    }
                }
               
            }
            user.Password = null;
            return user;
        }
          
        
    }
}
