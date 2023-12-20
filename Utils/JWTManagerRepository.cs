using marketplace_backend.DTO.Token;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Services.UserService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace marketplace_backend.Utils
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        private readonly IUserService _userService;
        public JWTManagerRepository(IConfiguration iconfiguration, IUserService userService)
        {
            this.iconfiguration = iconfiguration;
            _userService = userService;
        }
        public Tokens Authenticate(LoginUserDTO loginUserDTO)
        {
            User? user = _userService.GetUserByEmail(loginUserDTO.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, user.Password))
            {
                return null;
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Email, loginUserDTO.Email),
                     new Claim("UserId", user.IdUser.ToString()),
                     new Claim("Role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), IdUser = user.IdUser };

        }

        public int? AuthSelfOrAdminByUserId(ClaimsIdentity? claimPrincipal)
        {
            int userIdToken = int.Parse(claimPrincipal.FindFirst("UserId").Value);
            return userIdToken;
        }

        public bool AuthAdmin(ClaimsIdentity? claimPrincipal)
        {
            string userRole = claimPrincipal.FindFirst("Role").Value;
            if (userRole != "admin")
            {
                return false;
            }
            return true;
        }
    }
}
