using Microsoft.EntityFrameworkCore;

using FW.Microservices.Orders.API.Entities;

namespace FW.Microservices.Orders.API.DBAccess;

public sealed class OrderDbContext
	: DbContext
{
	public DbSet<Customer> Customers { get; set; }

	public DbSet<Order> Orders { get; set; }

	public DbSet<OrderDetails> OrderDetails { get; set; }

	public DbSet<OrderItem> OrderItems { get; set; }

	public OrderDbContext (DbContextOptions<OrderDbContext> options)
		: base (options)
	{
	}

	protected override void OnModelCreating (ModelBuilder modelBuilder)
	{
		// Configure the 1-to-1 relationship between Order and OrderDetails
		modelBuilder.Entity<Order> ()
			.HasOne (o => o.OrderDetails)
			.WithOne (od => od.Order)
			.HasForeignKey<OrderDetails> (od => od.OrderId); // Use OrderId as the unique FK

		// Configure the one-to-many relationship between Customer and Orders
		modelBuilder.Entity<Customer> ()
			.HasMany (c => c.Orders)
			.WithOne (o => o.Customer)
			.HasForeignKey (o => o.CustomerId);

		// Configure the one-to-many relationship between Order and OrderItems
		modelBuilder.Entity<Order> ()
			.HasMany (o => o.OrderItems)
			.WithOne (oi => oi.Order)
			.HasForeignKey (oi => oi.OrderId);
	}
}