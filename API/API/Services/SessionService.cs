using API.Models.Cart;
using API.Services.IServices;
using Newtonsoft.Json;

namespace API.Services
{
	public class SessionService : ISessionService
	{

		private const string CartSessionKey = "Cart"; // Clé pour identifier le panier dans la session

		public Cart GetCart(ISession session)
		{
			var cartJson = session.GetString(CartSessionKey); // Récupère le panier sous forme de JSON
			if (string.IsNullOrEmpty(cartJson))
			{
				return new Cart(); // Si aucun panier, on renvoie un panier vide
			}
			return JsonConvert.DeserializeObject<Cart>(cartJson); // Désérialise le JSON en objet Cart
		}

		public void SaveCart(ISession session, Cart cart)
		{
			var cartJson = JsonConvert.SerializeObject(cart); // Sérialise l'objet Cart en JSON
			session.SetString(CartSessionKey, cartJson); // Sauvegarde le panier dans la session
		}
	}
}
