using marketplace_backend.DTO.Item;
using marketplace_backend.Services.BaseItemService;
using marketplace_backend.Services.ItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]"), Authorize]
    public class ItemController : Controller
    {
        private IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] NewItemDTO newItemDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            ItemDTO? insertedBaseItem = await _itemService.AddOne(newItemDTO);
            if (insertedBaseItem == null) return BadRequest();
            return Created("", insertedBaseItem);
        }

        [HttpPut("{id_item}")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id_item, [FromBody] UpdatedItemDTO updatedItemDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            ItemDTO? itemDTO = await _itemService.UpdateOne(id_item, updatedItemDTO);
            if (itemDTO == null) return NotFound();
            return Ok(itemDTO);
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
