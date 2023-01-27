using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(Model.DTO.LoginRequest loginRequest)
        {
            // Create Login Model in DTO

            // Validate the Incoming Request

            // Check if user is authenticated
            // Check User Name and password 
            var isAuthenticated = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (isAuthenticated)
            {
                //Genrate a JWT Token

            }

            return BadRequest("Username or Password is incorrect");
        }
    }
}
