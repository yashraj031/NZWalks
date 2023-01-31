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
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(Model.DTO.LoginRequest loginRequest)
        {
            // Create Login Model in DTO

            // Validate the Incoming Request

            // Check if user is authenticated
            // Check User Name and password 
            var user = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                //Genrate a JWT Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);

            }

            return BadRequest("Username or Password is incorrect");
        }

       
    }
}
