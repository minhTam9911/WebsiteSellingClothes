using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Brand> Brands { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Contact> Contacts { get; set; }
	public DbSet<Discount> Discounts { get; set; }
	public DbSet<Favourite> Favourites { get; set; }
	public DbSet<Feedback> Feedbacks { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductImage> ProductImages { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<User> Users { get; set; }
}
