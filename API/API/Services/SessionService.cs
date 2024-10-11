using API.Models.Cart;
using API.Services.IServices;
using Newtonsoft.Json;

namespace API.Services
{
	public class SessionService : ISessionService
	{

		// Dictionnaire pour stocker les paniers en mémoire
		private static readonly Dictionary<string, Cart> _userCarts = new();

		public Cart GetCart(string userId)
		{
			// Récupérer le panier de l'utilisateur ou en créer un nouveau
			if (!_userCarts.TryGetValue(userId, out var cart))
			{
				cart = new Cart();
				_userCarts[userId] = cart;
			}
			return cart;
		}

		public void SaveCart(string userId, Cart cart)
		{
			// Sauvegarder le panier dans le dictionnaire
			_userCarts[userId] = cart;
		}
	}
}
