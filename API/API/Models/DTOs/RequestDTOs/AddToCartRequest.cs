namespace API.Models.DTOs.RequestDTOs
{
	public class AddToCartRequest
	{
		public Guid ItemId { get; set; }  // ID de l'article à ajouter au panier
		public int Quantity { get; set; }  // Quantité de l'article à ajouter
	}
}
