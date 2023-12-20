using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByEmail(string email);
        Task<User?> UpdateOne(int idUser, UpdatedUserDTO updatedUserDTO);
        bool UserAddressExists(int idUser);
    }
}
