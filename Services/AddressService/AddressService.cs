using marketplace_backend.DTO;
using marketplace_backend.DTO.Address;
using marketplace_backend.Models;
using marketplace_backend.Repository.AddressRepository;
using marketplace_backend.Repository.BaseItemRepository;
using marketplace_backend.Repository.UserRepository;
using marketplace_backend.Services.UserService;
using marketplace_backend.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_backend.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _addressRepository;
        private IUserRepository _userRepository;
        private ITypeConverter _typeConverter;

        public AddressService(ITypeConverter typeConverter)
        {
            _addressRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).AddressRepository;
            _typeConverter = typeConverter;
            _userRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).UserRepository;
        }

        public async Task<AddressDTO?> AddOne(int idUser, NewAddressDTO newAddressDTO)
        {
            if (_userRepository.UserAddressExists(idUser)) return null;
            Address address = _typeConverter.NewAddressDTOToAddress(newAddressDTO);
            address.IdUser = idUser;
            Address? insertedAddress = await _addressRepository.InsertAsync(address);
            if (insertedAddress == null) return null;
            AddressDTO addressDTO = _typeConverter.AddressToAddressDTO(insertedAddress);
            return addressDTO;
        }

        public async Task<AddressDTO?> UpdateOne(int idUser, NewAddressDTO newAddressDTO)
        {
            Address? address = await _addressRepository.UpdateOne(idUser, newAddressDTO);
            if (address == null) return null;
            AddressDTO addressDTO = _typeConverter.AddressToAddressDTO(address);
            return addressDTO;
        }
    }
}
