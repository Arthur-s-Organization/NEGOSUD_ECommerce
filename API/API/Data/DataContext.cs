using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<AlcoholFamily> AlcoholFamilies { get; set; }
		public DbSet<AlcoholItem> AlcoholItems { get; set; }
		public DbSet<CommonItem> CommonItems { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<SupplierOrder> SupplierOrders { get; set; }

	}
}
