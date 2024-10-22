namespace API.Models.DTOs.RequestDTOs
{
	public class RemoveFromCartRequest
	{
		public Guid ItemId { get; set; }  // ID de l'article à retirer du panier
	}
}
