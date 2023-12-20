using marketplace_backend.DTO;
using marketplace_backend.DTO.CartLine;
using marketplace_backend.Models;
using marketplace_backend.Services.CartLineService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class CartLineController : ControllerBase
    {
        private ICartLineService _cartLineService;
        private ITypeConverter _typeConverter;

        public CartLineController(ITypeConverter typeConverter, ICartLineService cartLineService)
        {
            _cartLineService = cartLineService;
            _typeConverter = typeConverter;
        }

        [HttpPost("{id_item}")]
        public async Task<IActionResult> AddOne([FromRoute] int id_item)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();

            CartLine? cartLine = await _cartLineService.AddOne(idUser.Value, id_item);
            if (cartLine == null) return BadRequest();

            return Created("", null);
        }

        [HttpPut("{id_item}/{quantity}")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id_item, [FromRoute] int quantity)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();

            CartLineDTO? cartLineDTO = await _cartLineService.UpdateOne(idUser.Value, id_item, quantity);
            if (cartLineDTO == null) return BadRequest();
            return Ok(cartLineDTO);
        }

        [HttpPut("{id_item}/{size}/{color}")]
        public async Task<IActionResult> UpdateOneItemSizeOrColor([FromRoute] int id_item, [FromRoute] string size, [FromRoute] string color)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();

            CartLineDTO? cartLineDTO = await _cartLineService.UpdateOneItemSizeOrColor(idUser.Value, id_item, size, color);
            if (cartLineDTO == null) return BadRequest();
            return Ok(cartLineDTO);
        }

        [HttpDelete("{id_item}")]
        public async Task<IActionResult> DeleteOne([FromRoute] int id_item)
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();
            if (await _cartLineService.DeleteOne((int)idUser, id_item)) return Ok();
            return NotFound();
        }

        [HttpGet("{id_cart}/{id_item}")]
        public async Task<IActionResult>getOne([FromRoute] int id_cart, [FromRoute] int id_item)
        {
            int? idUser = AuthSelf();
            if(idUser == null) return Unauthorized();
            CartLine cartLine =  await _cartLineService.GetOne(id_cart, id_item);
            return Ok(cartLine);
        }

        private int? AuthSelf()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            int userIdToken = int.Parse(claimsIdentity.FindFirst("UserId").Value);
            return userIdToken;
        }
    }
}
