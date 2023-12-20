using marketplace_backend.DTO.Item;
using marketplace_backend.DTO.ItemImage;
using marketplace_backend.Services.ItemImageService;
using marketplace_backend.Services.ItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]"), Authorize]
    public class ItemImageController : Controller
    {
        private IItemImageService _itemImageService;

        public ItemImageController(IItemImageService itemImageService)
        {
            _itemImageService = itemImageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] NewItemImageDTO newItemImageDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            ItemImageDTO itemImageDTO = await _itemImageService.AddOne(newItemImageDTO);
            if (itemImageDTO == null) return BadRequest();
            return Created("", itemImageDTO);
        }

        [HttpPut("{id_item_image}")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id_item_image, [FromBody] UpdatedItemImageDTO updatedItemImageDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            ItemImageDTO? itemImageDTO = await _itemImageService.UpdateOne(id_item_image, updatedItemImageDTO);
            if (itemImageDTO == null) return NotFound();
            return Ok(itemImageDTO);
        }

        [HttpDelete("{id_item_image}")]
        public IActionResult DeleteOne([FromRoute] int id_item_image)
        {
            if (!AuthAdmin()) return Unauthorized();
            _itemImageService.DeleteOne(id_item_image);
            return Ok();
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
