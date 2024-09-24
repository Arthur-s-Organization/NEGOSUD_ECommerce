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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			// Gestion des relations d'héritage
			modelBuilder.Entity<Item>().ToTable("Items");
			modelBuilder.Entity<AlcoholItem>().ToTable("AlcoholItem");
			modelBuilder.Entity<CommonItem>().ToTable("CommonItem");

			modelBuilder.Entity<Order>().ToTable("Order");
			modelBuilder.Entity<SupplierOrder>().ToTable("SupplierOrder");
			modelBuilder.Entity<CustomerOrder>().ToTable("CustomerOrder");


			// Gestion des relations One-To-One
			modelBuilder.Entity<Customer>()
				.HasOne(c => c.Address)
				.WithOne(a => a.Customer)
				.HasForeignKey<Address>(a => a.CustomerId);

			modelBuilder.Entity<Supplier>()
				.HasOne(s => s.Address)
				.WithOne(a => a.Supplier)
				.HasForeignKey<Address>(a => a.SupplierId);


			// Gestion des relations One-To-Many
			modelBuilder.Entity<CustomerOrder>()
				.HasOne(co => co.Customer)
				.WithMany(c => c.CustomerOrders)
				.HasForeignKey(co => co.CustomerId);

			modelBuilder.Entity<Item>()
				.HasOne(i => i.Supplier)
				.WithMany(s => s.Items)
				.HasForeignKey(i => i.SupplierId);

			modelBuilder.Entity<SupplierOrder>()
				.HasOne(so => so.Supplier)
				.WithMany(s => s.SupplierOrders)
				.HasForeignKey(so => so.SupplierId);

			modelBuilder.Entity<AlcoholItem>()
				.HasOne(ai => ai.AlcoholFamily)
				.WithMany(af => af.AlcoholItems)
				.HasForeignKey(ai => ai.AlcoholFamilyId);


			// Gestion des relations Many-To-Many
			modelBuilder.Entity<OrderDetail>()
				.HasKey(od => new { od.OrderId, od.ItemId });

			modelBuilder.Entity<OrderDetail>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderDetails)
				.HasForeignKey(oi => oi.OrderId);

			modelBuilder.Entity<OrderDetail>()
				.HasOne(oi => oi.Item)
				.WithMany(i => i.OrderDetails)
				.HasForeignKey(oi => oi.ItemId);

		}
	}
}
