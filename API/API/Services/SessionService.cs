using API.Models.Cart;
using API.Services.IServices;
using Newtonsoft.Json;

namespace API.Services
{
	public class SessionService : ISessionService
	{

		//// Dictionnaire pour stocker les paniers en mémoire
		//private static readonly Dictionary<string, Cart> _userCarts = new();

		//public Cart GetCart(string userId)
		//{
		//	// Récupérer le panier de l'utilisateur ou en créer un nouveau
		//	if (!_userCarts.TryGetValue(userId, out var cart))
		//	{
		//		cart = new Cart();
		//		_userCarts[userId] = cart;
		//	}
		//	return cart;
		//}

		//public void SaveCart(string userId, Cart cart)
		//{
		//	// Sauvegarder le panier dans le dictionnaire
		//	_userCarts[userId] = cart;
		//}



		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public Cart GetCart(string userId)
		{
			// Récupérer la session de l'utilisateur
			var session = _httpContextAccessor.HttpContext.Session;

			// Récupérer le panier depuis la session
			var cartJson = session.GetString(userId);
			if (cartJson == null)
			{
				// Si le panier n'existe pas, en créer un nouveau
				return new Cart();
			}

			// Désérialiser le panier stocké en JSON
			return JsonConvert.DeserializeObject<Cart>(cartJson);
		}

		public void SaveCart(string userId, Cart cart)
		{
			// Sérialiser le panier en JSON
			var cartJson = JsonConvert.SerializeObject(cart);

			// Enregistrer le panier dans la session de l'utilisateur
			var session = _httpContextAccessor.HttpContext.Session;
			session.SetString(userId, cartJson);
		}
	}
}
