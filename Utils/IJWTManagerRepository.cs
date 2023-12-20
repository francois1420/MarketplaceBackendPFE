using marketplace_backend.DTO.Token;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using System.Security.Claims;

namespace marketplace_backend.Utils
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(LoginUserDTO loginUserDTO);
        int? AuthSelfOrAdminByUserId(ClaimsIdentity? claimPrincipal);
        bool AuthAdmin(ClaimsIdentity? claimPrincipal);
    }
}
