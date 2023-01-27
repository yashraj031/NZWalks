using NZWalksAPI.Model.Domain;

namespace NZWalksAPI.Repository
{
    public class StaticUserRepository : IUserRepository
    {


        private List<User> Users = new List<User>()
        {

            new User()
            {
                FirstName = "Read Only", LastName = "User", EmailAddress = "readonly@user.com",
                Id = Guid.NewGuid(), UserName = "readonly@user.com", Password = "Readonly@user",
                Roles = new List<string>    { "reader" }
            },
            new User()
            {
                FirstName = "Read Write", LastName = "User", EmailAddress = "readwrite@user.com",
                Id = Guid.NewGuid(), UserName = "readwrite@user.com", Password = "Readwrite@user",
                Roles = new List<string> { "reader", "writer" }
            }
        };
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            x.Password == password);

            if (user != null) 
            {
                return true;
            }
            return false;
            
        }
    }
}
