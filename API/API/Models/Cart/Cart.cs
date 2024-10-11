namespace API.Models.Cart
{
	public class Cart
	{
		public List<CartItem> Items { get; set; } = new List<CartItem>();

		public void AddItem(Guid itemId, int quantity)
		{
			var existingItem = Items.Find(i => i.ItemId == itemId);
			if (existingItem != null)
			{
				existingItem.Quantity += quantity;
			}
			else
			{
				Items.Add(new CartItem { ItemId = itemId, Quantity = quantity });
			}
		}

		public void RemoveItem(Guid itemId)
		{
			Items.RemoveAll(i => i.ItemId == itemId);
		}
	}
}
