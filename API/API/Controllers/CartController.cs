using API.Models.DTOs.RequestDTOs;
using API.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ISessionService _sessionService; // Service pour gérer les sessions

		public CartController(ISessionService sessionService)
		{
			_sessionService = sessionService;
		}

		[HttpPost("add")]
		public IActionResult AddToCart([FromBody] AddToCartRequest request)
		{
			var cart = _sessionService.GetCart(HttpContext.Session);
			cart.AddItem(request.ItemId, request.Quantity);
			_sessionService.SaveCart(HttpContext.Session, cart);
			return Ok(cart);
		}

		[HttpDelete("remove")]
		public IActionResult RemoveFromCart([FromBody] RemoveFromCartRequest request)
		{
			var cart = _sessionService.GetCart(HttpContext.Session);
			cart.RemoveItem(request.ItemId);
			_sessionService.SaveCart(HttpContext.Session, cart);
			return Ok(cart);
		}

		[HttpGet]
		public IActionResult GetCart()
		{
			var cart = _sessionService.GetCart(HttpContext.Session);
			return Ok(cart);
		}

	}
}
