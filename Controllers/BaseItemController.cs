using marketplace_backend.DTO.BaseItem;
using marketplace_backend.Models;
using marketplace_backend.Services.BaseItemService;
using marketplace_backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [ApiController, Authorize]
    [Route("/api/[controller]")]
    public class BaseItemController : Controller
    {
        private IBaseItemService _baseItemService;

        public BaseItemController(IBaseItemService baseItemService) 
        {
            _baseItemService = baseItemService;
        }

        [HttpGet]
        [Route("{id_base_item}"), AllowAnonymous]
        public async Task<IActionResult> GetOneById([FromRoute] int id_base_item)
        {
            BaseItemDTO? item = await _baseItemService.GetOne(id_base_item);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IList<BaseItemDTO>> GetAll([FromQuery] int? category, [FromQuery] string? nameOrDescription)
        {
            return await _baseItemService.GetAll(category, nameOrDescription);
        }

        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] NewBaseItemDTO baseItemDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            InsertedBaseItemDTO? insertedBaseItem = await _baseItemService.AddOne(baseItemDTO);
            if (insertedBaseItem == null) return BadRequest();
            return Created("", insertedBaseItem);
        }

        [HttpPut("{id_base_item}")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id_base_item, [FromBody] NewBaseItemDTO baseItemDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            BaseItemDTO updatedBaseItemDTO = await _baseItemService.UpdateOne(id_base_item, baseItemDTO);
            if (updatedBaseItemDTO == null) return NotFound();
            return Ok(updatedBaseItemDTO);
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
