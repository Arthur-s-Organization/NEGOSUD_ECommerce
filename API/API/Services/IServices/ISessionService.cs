using API.Models.Cart;

namespace API.Services.IServices
{
	public interface ISessionService
	{
		Cart GetCart(string userId);
		void SaveCart(string userId, Cart cart);
	}
}
