using marketplace_backend.DTO.Cart;
using marketplace_backend.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]"), Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("{id_user}")]
        public IActionResult GetActualCartByUserId([FromRoute] int id_user)
        {
            if (!AuthAdmin()) return Unauthorized();
            CartDTO? cartDTO = _cartService.GetActualCartByUserId(id_user);
            if (cartDTO == null) return NotFound();
            return Ok(cartDTO);
        }

        [HttpGet]
        [Route("me")]
        public IActionResult GetActualCart()
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();
            CartDTO? cartDTO = _cartService.GetActualCartByUserId(idUser.Value);
            if (cartDTO == null) return NotFound();
            return Ok(cartDTO);
        }

        [HttpGet]
        [Route("{id_user}/history")]
        public IActionResult GetBuyHistoryByUserId([FromRoute] int id_user)
        {
            if (!AuthAdmin()) return Unauthorized();
            return Ok(_cartService.GetHistoryCartsByUserId(id_user));
        }

        [HttpGet]
        [Route("me/history")]
        public IActionResult GetBuyHistory()
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();
            return Ok(_cartService.GetHistoryCartsByUserId(idUser.Value));
        }

        // GET /carts/buy - creates a payment intent with the client secret - Self
        [HttpGet]
        [Route("buy")]
        public IActionResult CreatePaymentIntent()
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();
            string clientSecret = _cartService.GetClientSecret(idUser.Value);
            return Ok(clientSecret);
        }


        // POST /carts/buy/success - Update cart info and create new cart in case of success - Self
        [HttpPost]
        [Route("buy/success")]
        public async Task<IActionResult> BuyCart()
        {
            int? idUser = AuthSelf();
            if (idUser == null) return Unauthorized();
            bool isBought = await _cartService.BuyCart(idUser.Value);
            if (!isBought) return BadRequest();
            return Ok();
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
