using API.Models.Cart;

namespace API.Services.IServices
{
	public interface ISessionService
	{
		Cart GetCart(ISession session);
		void SaveCart(ISession session, Cart cart);
	}
}
