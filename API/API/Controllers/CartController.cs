using API.Models;
using API.Models.DTOs.RequestDTOs;
using API.Services;
using API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{

	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ISessionService _sessionService;
		private readonly IItemService _itemService;

		public CartController(ISessionService sessionService, IItemService itemService)
		{
			_sessionService = sessionService;
			_itemService = itemService;
		}

		[HttpPost("add")]
		public IActionResult AddToCart([FromBody] AddToCartRequest request)
		{
			// Récupérer l'ID de l'utilisateur authentifié
			//var userId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("User is not authenticated.");
			}
			var cart = _sessionService.GetCart(userId);

			var item = _itemService.GetItemById(request.ItemId);
			if (item == null)
			{
				return NotFound("Item not found.");
			}

			cart.AddItem(item, request.Quantity);
			_sessionService.SaveCart(userId, cart);
			return Ok(cart);
		}

		[HttpDelete("remove")]
		public IActionResult RemoveFromCart([FromBody] RemoveFromCartRequest request)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cart = _sessionService.GetCart(userId);


			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("User is not authenticated.");
			}

			cart.RemoveItem(request.ItemId);
			_sessionService.SaveCart(userId, cart);
			return Ok(cart);
		}

		[HttpGet]
		public IActionResult GetCart()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cart = _sessionService.GetCart(userId);
			return Ok(cart);
		}

		[HttpPut("update")]
		public IActionResult UpdateCart([FromBody] AddToCartRequest request)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("User is not authenticated.");
			}
			var cart = _sessionService.GetCart(userId);

			var item = _itemService.GetItemById(request.ItemId);
			if (item == null)
			{
				return NotFound("Item not found.");
			}


			cart.UpdateItem(item, request.Quantity);
			_sessionService.SaveCart(userId, cart);
			return Ok(cart);
		}

	}
}
