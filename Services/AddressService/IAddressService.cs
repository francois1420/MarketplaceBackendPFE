using marketplace_backend.DTO.Address;

namespace marketplace_backend.Services.AddressService
{
    public interface IAddressService
    {
        Task<AddressDTO?> AddOne(int idUser, NewAddressDTO newAddressDTO);
        Task<AddressDTO?> UpdateOne(int idUser, NewAddressDTO newAddressDTO);
    }
}
