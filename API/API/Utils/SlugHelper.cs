namespace API.Utils
{
	public class SlugHelper
	{
		public static string GenerateSlug(string itemName)
		{
			return itemName.ToLower().Replace(" ", "-").Replace(",", "").Replace(".", "");
		}
	}
}
