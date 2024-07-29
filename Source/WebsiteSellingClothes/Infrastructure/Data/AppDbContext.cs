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
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentDestination> PaymentDestinations { get; set; }
    public DbSet<PaymentNotification> PaymentNotifications { get; set; }
    public DbSet<PaymentSignature> PaymentSignatures { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Role> Roles { get; set; }
    //public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
       new Role() { Id = 1, Name = "Admin", Description = "This is the highest authority",Created = DateTime.Now, Updated = DateTime.Now },
       new Role() { Id = 2, Name = "User", Description = "This is the user's permission",Created = DateTime.Now, Updated = DateTime.Now });
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = Guid.Parse("1c7b198d-d9f7-4fac-823c-dd6ea13e44de"),
                Address = "TP HCM",
                CreatedDate = DateTime.Now,
                DateOfBirth = DateTime.Parse("2003-07-29 00:00:00"),
                Email = "minhtamceo1@gmail.com",
                FullName = "Minh Tam",
                Gender ="Male",
                Image = string.Empty,
                IsActive = true,
                Password = "$2y$12$9E2GHspbgnWShdr2gqpof.r9vHoBcQrZvZixUAEz7vKlBG47dUigS", //Minhtamceo1
                PhoneNumber = "0816098920",
                RefeshTokenExpires = null,
                RefreshToken = string.Empty,
                RoleId = 1,
                SecurityCode = string.Empty,
                TokenResetPassword = string.Empty,
                TokenResetPasswordExpires =null,
                UpdatedDate =DateTime.Now,
                Username = "minhtamceo1"
            }
            );
        modelBuilder.Entity<PaymentDestination>().HasData(
            new PaymentDestination() { 
                Id = "DEST001",
                DestinationLogo = "https://admin.softmaster.vn/_default_upload_bucket/154573132_152687123342645_1913382004205201124_n.png",
                DestinationName = "Công ty Cổ phần Giải pháp Thanh toán Việt Nam",
                DestinationShortName = "VNPAY",
                IsActive = true,
                PaymentDestinationParent = null,
            },
            new PaymentDestination()
            {
                Id = "DEST002",
                DestinationLogo = "https://upload.wikimedia.org/wikipedia/vi/f/fe/MoMo_Logo.png",
                DestinationName = "CÔNG TY CỔ PHẦN DỊCH VỤ DI ĐỘNG TRỰC TUYẾN",
                DestinationShortName = "MOMO",
                IsActive = true,
                PaymentDestinationParent = null,
            },
            new PaymentDestination()
            {
                Id = "DEST003",
                DestinationLogo = "https://cdn.haitrieu.com/wp-content/uploads/2022/10/Logo-ZaloPay-Square.png",
                DestinationName = "Công ty Cổ phần ZION",
                DestinationShortName = "ZALOPAY",
                IsActive = true,
                PaymentDestinationParent = null,
            }
            );
        modelBuilder.Entity<Merchant>().HasData(
            new Merchant() {
                Id = "MER001",
                MerchantName = "My Website",
                MerchantWebLink = "https://www.mywebsite.com",
                MerchantIpnUrl = "https://www.mywebsite.com/ipn",
                MerchantReturnUrl = "https://www.mywebsite.com/return",
                SercetKey = "MIICWgIBAAKBgEyYcW79ojjWADa+6xnbjj8CInqsanIIRwO6mbefco7ivjksaQGM",
                IsActive = true,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                UserId = Guid.Parse("1c7b198d-d9f7-4fac-823c-dd6ea13e44de")

            }
            );
    }
}
