using marketplace_backend.DTO;
using marketplace_backend.DTO.Cart;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repository.UserRepository;
using marketplace_backend.Services.CartService;
using marketplace_backend.UnitsOfWork;

namespace marketplace_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ICartService _cartService;
        private ITypeConverter _typeConverter;

        public UserService(ITypeConverter typeConverter, ICartService cartService)
        {
            _userRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).UserRepository;
            _typeConverter = typeConverter;
            _cartService = cartService;
        }

        public async Task<UserDTO?> GetOne(int id)
        {
            User? user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;
            return _typeConverter.UserToUserDTO(user);
        }

        public async Task<IList<UserDTO>> GetAll(string? firstName, string? lastName, string? email)
        {
            return (await _userRepository
            .SearchForAsync(user =>
                (firstName == null || user.FirstName.ToLower().Contains(firstName.ToLower()))
                && (lastName == null || user.LastName.ToLower().Contains(lastName.ToLower()))
                && (email == null || user.Email.ToLower().Contains(email.ToLower()))))
            .Select(user => _typeConverter.UserToUserDTO(user))
            .ToList();
        }

        public async Task<User?> AddOne(NewUserDTO userDTO)
        {
            User? user = await _userRepository.InsertAsync(_typeConverter.UserDTOToUser(userDTO));
            if (user == null) return null;
            await _cartService.AddOne(user.IdUser);
            return user;
        }

        public async Task<UserDTO?> UpdateOne(int id, UpdatedUserDTO updatedUserDTO)
        {
            User? user = await _userRepository.UpdateOne(id, updatedUserDTO);
            if (user == null) return null;
            return _typeConverter.UserToUserDTO(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
    }
}