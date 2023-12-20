using marketplace_backend.DTO.Address;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.AddressRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address?> UpdateOne(int idUser, NewAddressDTO newAddressDTO);
    }
}
