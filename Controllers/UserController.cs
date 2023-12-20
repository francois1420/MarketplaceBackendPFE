using marketplace_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using marketplace_backend.Services.UserService;
using marketplace_backend.DTO.User;
using marketplace_backend.Utils;
using marketplace_backend.DTO.Token;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]"), Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IUserService userService, IJWTManagerRepository jWTManager)
        {
            _userService = userService;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        [Route("{id_user}")]
        public async Task<IActionResult> GetOneById([FromRoute] int id_user)
        {
            if (!AuthAdmin()) return Unauthorized();

            UserDTO? retrievedUser = await _userService.GetOne(id_user);
            if (retrievedUser == null) return NotFound();
            return Ok(retrievedUser);
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetMe()
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();

            UserDTO? retrievedUser = await _userService.GetOne(idUser.Value);
            if (retrievedUser == null) return NotFound();
            return Ok(retrievedUser);
        }

        [HttpGet]
        public async Task<IActionResult> getAll([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? email)
        {
            if (!AuthAdmin()) return Unauthorized();
            return Ok(await _userService.GetAll(firstName, lastName, email));
        }

        [HttpPut("{id_user}")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id_user, [FromBody] UpdatedUserDTO updatedUserDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            UserDTO? userDTO = await _userService.UpdateOne(id_user, updatedUserDTO);
            if (userDTO == null) return NotFound();
            return Ok(userDTO);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMe([FromBody] UpdatedUserDTO updatedUserDTO)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();
            UserDTO? userDTO = await _userService.UpdateOne(idUser.Value, updatedUserDTO);
            if (userDTO != null) return Ok(userDTO);
            return NotFound();
        }


        [HttpPost, AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] NewUserDTO user)
        {
            //System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
            string unprotectedPassword = user.Password;
            user.Password = BCrypt.Net.BCrypt.HashPassword(unprotectedPassword);
            User? insertedUser = await _userService.AddOne(user);
            if (insertedUser == null) return Conflict();

            Tokens? token = _jWTManager.Authenticate(new LoginUserDTO { Email = user.Email, Password = unprotectedPassword });
            return Created("", token);
        }
        
        [HttpPost, AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] LoginUserDTO loginUserDTO)
        {
            Tokens? token = _jWTManager.Authenticate(loginUserDTO);
            if (token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }

        private int? AuthSelf()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            int userIdToken = int.Parse(claimsIdentity.FindFirst("UserId").Value);
            return userIdToken;
        }

        private bool AuthAdmin()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string userRole = claimsIdentity.FindFirst("Role").Value;
            if (userRole != "admin")
            {
                return false;
            }
            return true;
        }
    }
}
