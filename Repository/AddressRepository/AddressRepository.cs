using marketplace_backend.DTO.Address;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;
using marketplace_backend.Repository.BaseItemRepository;
using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.Repository.AddressRepository
{
    public class AddressRepository : BaseRepositorySQL<Address>, IAddressRepository
    {
        public AddressRepository(MarketplaceBackendDbContext context) : base(context) { }

        public async Task<Address?> UpdateOne(int idUser, NewAddressDTO newAddressDTO)
        {
            Address? address = (from Address a in context.Set<Address>()
                                where a.IdUser == idUser
                                select a).FirstOrDefault();

            if (address == null) return null;

            address.StreetNumber = newAddressDTO.StreetNumber;

            if (address.AppartmentNumber == null)
                address.AppartmentNumber = newAddressDTO.AppartmentNumber;

            address.StreetName = newAddressDTO.StreetName;
            address.City = newAddressDTO.City;
            address.State = newAddressDTO.State;
            address.ZipCode = newAddressDTO.ZipCode;
            address.Country = newAddressDTO.Country;

            await SaveChangesAsync();

            return address;
        }
    }
}
