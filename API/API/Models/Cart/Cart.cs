using Azure.Core;

namespace API.Models.Cart
{
	public class Cart
	{
		public List<CartItem> Items { get; set; } = new List<CartItem>();

		public void AddItem(Item item, int quantity)
		{
			var existingItem = Items.Find(i => i.Item.ItemId == item.ItemId);
			if (existingItem != null)
			{
				existingItem.Quantity += quantity;
			}
			else
			{
				Items.Add(new CartItem { Item = item, Quantity = quantity });
			}
		}

		public void RemoveItem(Guid itemId)
		{
			Items.RemoveAll(i => i.Item.ItemId == itemId);
		}

		public void UpdateItem(Item item, int quantity)
		{
			var existingItem = Items.Find(i => i.Item.ItemId == item.ItemId);
			if (existingItem != null)
			{
				existingItem.Quantity = quantity;
			}

		}
	}
}
