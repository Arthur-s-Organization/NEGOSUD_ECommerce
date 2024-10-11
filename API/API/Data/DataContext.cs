using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class DataContext : IdentityDbContext<IdentityUser>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<AlcoholFamily> AlcoholFamilies { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<SupplierOrder> SupplierOrders { get; set; }
		public DbSet<CustomerOrder> CustomerOrders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Order>().ToTable("Order");
			modelBuilder.Entity<SupplierOrder>().ToTable("SupplierOrder");
			modelBuilder.Entity<CustomerOrder>().ToTable("CustomerOrder");



			modelBuilder.Entity<Address>()
				.HasOne(a => a.Customer)
				.WithOne(c => c.Address)
				.HasForeignKey<Customer>(c => c.AddressId);

			modelBuilder.Entity<Address>()
				.HasOne(a => a.Supplier)
				.WithOne(s => s.Address)
				.HasForeignKey<Supplier>(s => s.AddressId);

			// Gestion des relations One-To-Many
			modelBuilder.Entity<CustomerOrder>()
				.HasOne(co => co.Customer)
				.WithMany(c => c.CustomerOrders)
				.HasForeignKey(co => co.CustomerId);

			modelBuilder.Entity<Item>()
				.HasOne(i => i.AlcoholFamily)
				.WithMany(af => af.Items)
				.HasForeignKey(i => i.AlcoholFamilyId);

			modelBuilder.Entity<Item>()
				.HasOne(i => i.Supplier)
				.WithMany(s => s.Items)
				.HasForeignKey(i => i.SupplierId);

			modelBuilder.Entity<SupplierOrder>()
				.HasOne(so => so.Supplier)
				.WithMany(s => s.SupplierOrders)
				.HasForeignKey(so => so.SupplierId);


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
