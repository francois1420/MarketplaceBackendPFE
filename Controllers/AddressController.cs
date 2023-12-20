using marketplace_backend.DTO.Address;
using marketplace_backend.Services.AddressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]"), Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] NewAddressDTO newAddressDTO)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();

            AddressDTO? addressDTO = await _addressService.AddOne(idUser.Value, newAddressDTO);
            if (addressDTO == null) return BadRequest();
            return Created("", addressDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOne([FromBody] NewAddressDTO newAddressDTO)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();

            AddressDTO? addressDTO = await _addressService.UpdateOne(idUser.Value, newAddressDTO);
            if (addressDTO == null) return BadRequest();
            return Created("", addressDTO);
        }

        private int? AuthSelf()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            int userIdToken = int.Parse(claimsIdentity.FindFirst("UserId").Value);
            return userIdToken;
        }

        private bool AuthAdmin()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string userRole = claimsIdentity.FindFirst("Role").Value;
            if (userRole != "admin")
            {
                return false;
            }
            return true;
        }
    }
}
