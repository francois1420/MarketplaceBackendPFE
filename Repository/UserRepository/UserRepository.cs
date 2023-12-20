using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;
using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.Repository.UserRepository
{
    public class UserRepository : BaseRepositorySQL<User>, IUserRepository
    {
        public UserRepository(MarketplaceBackendDbContext context) : base(context) { }

        public User? GetUserByEmail(string email)
        {
            User? user = (from User u in context.Set<User>()
                          where u.Email == email
                          select u).FirstOrDefault();
            return user;
        }

        public bool UserAddressExists(int idUser)
        {
            User? user = (from User u in context.Set<User>()
                                         where u.IdUser == idUser
                                         select u).FirstOrDefault();

            if (user == null) return false;
            if (user.Addresses.Count == 0) return false;
            return true;
        }

        public async Task<User?> UpdateOne(int idUser, UpdatedUserDTO updatedUserDTO)
        {
            User? user = (from User u in context.Set<User>()
                          where u.IdUser == idUser
                          select u).FirstOrDefault();

            user.FirstName = updatedUserDTO.FirstName; 
            user.LastName = updatedUserDTO.LastName;
            user.Email = updatedUserDTO.Email;
            if (updatedUserDTO.PhoneNumber != null) 
                user.PhoneNumber = updatedUserDTO.PhoneNumber;


            await SaveChangesAsync();

            return user;
        }
    }
}
