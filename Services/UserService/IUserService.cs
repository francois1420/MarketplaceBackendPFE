using marketplace_backend.DTO.BaseItem;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_backend.Services.UserService
{
    public interface IUserService
    {
        Task<UserDTO?> GetOne(int id);
        Task<IList<UserDTO>> GetAll(string? firstName, string? lastName, string? email);
        Task<User?> AddOne(NewUserDTO userDTO);
        Task<UserDTO?> UpdateOne(int id, UpdatedUserDTO updatedUserDTO);
        User? GetUserByEmail(string email);
    }
}
