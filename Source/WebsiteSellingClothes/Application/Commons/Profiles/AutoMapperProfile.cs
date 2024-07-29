using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using Application.Features.PaymentFeatures.Commands.ProcessVnPay;
using AutoMapper;
using Domain.Entities;

namespace Application.Commons.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<VnPayResponseDto, ProcessVnPayPaymentCommand>();
        CreateMap<BrandRequestDto, Brand>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Trim()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));
        CreateMap<Brand, BrandResponseDto>()
            .ForMember(destination => destination.Image, option => option.MapFrom(src => BaseUrl("brand") + src.Image));
        CreateMap<CartRequestDto, Cart>();
        CreateMap<Cart, CartResponseDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product!.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<ContactRequestDto, Contact>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.Trim()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Trim()))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Trim()));
        CreateMap<CategoryRequestDto, Category>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Trim()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));
        CreateMap<Category, CategoryResponseDto>()
           .ForMember(destination => destination.Image, option => option.MapFrom(src => BaseUrl("category") + src.Image));
        CreateMap<Contact, ContactResponseDto>();
        CreateMap<DiscountRequestDto, Discount>();
        CreateMap<Discount, DiscountResponseDto>()
            .ForMember(dest => dest.ProductsId, opt => opt.MapFrom(src => src.Products!.Select(x => x.Id).ToList()));
        CreateMap<FavouriteRequestDto, Favourite>();
        CreateMap<Favourite, FavourireResponseDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product!.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<FeedbackRequestDto, Feedback>()
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment.Trim()));
        CreateMap<Feedback, FeedbackResponseDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product!.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<OrderDetailRequestDto, OrderDetail>();
        CreateMap<OrderDetail, OrderDetailResponseDto>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order!.Id))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product!.Id))
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product!.Code))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<OrderRequestDto, Order>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Trim()));
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment!.Id))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Trim()))
            .ForMember(dest => dest.OrderDetailsId, opt => opt.MapFrom(src => src.OrderDetails!.Select(x => x.Id).ToList()))
            .ForMember(dest=>dest.DiscountId, opt=>opt.MapFrom(src=>src.Discount!.Id ?? string.Empty))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<MerchantRequestDto, Merchant>();
        CreateMap<Merchant, MerchantResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<PaymentRequestDto, Payment>()
            .BeforeMap((src, dst) =>
            {
                foreach (var property in src.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(src)!;
                        property.SetValue(src, value!.Trim());
                    }
                }
            })
            .AfterMap((src, dst) =>
            {
                foreach (var property in dst.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(dst)!;
                        property.SetValue(dst, value!.Trim());
                    }
                }
            });
        CreateMap<Payment, PaymentResponseDto>()
            .ForMember(dest => dest.MerchantId, opt => opt.MapFrom(src => src.Merchant!.Id))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order!.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id));
        CreateMap<PaymentDestinationRequestDto, PaymentDestination>()
            .BeforeMap((src, dst) =>
            {
                foreach (var property in src.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(src)!;
                        property.SetValue(src, value!.Trim());
                    }
                }
            })
            .AfterMap((src, dst) =>
            {
                foreach (var property in dst.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(dst)!;
                        property.SetValue(dst, value!.Trim());
                    }
                }
            }); ;
        CreateMap<PaymentDestination, PaymentDestinationResponseDto>()
            .ForMember(dest => dest.PaymentDestinationParentId, opt => opt.MapFrom(src => src.PaymentDestinationParent == null ? string.Empty : src.PaymentDestinationParent.Id));
        CreateMap<PaymentNotificationRequestDto, PaymentNotification>()
            .BeforeMap((src, dst) =>
            {
                foreach (var property in src.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(src)!;
                        property.SetValue(src, value!.Trim());
                    }
                }
            })
            .AfterMap((src, dst) =>
            {
                foreach (var property in dst.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(dst)!;
                        property.SetValue(dst, value!.Trim());
                    }
                }
            }); ;
        CreateMap<PaymentNotification, PaymentNotificationResponseDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment!.Id));
        CreateMap<PaymentSignatureRequestDto, PaymentSignature>()
            .BeforeMap((src, dst) =>
            {
                foreach (var property in src.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(src)!;
                        property.SetValue(src, value!.Trim());
                    }
                }
            })
            .AfterMap((src, dst) =>
            {
                foreach (var property in dst.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(dst)!;
                        property.SetValue(dst, value!.Trim());
                    }
                }
            }); ;
        CreateMap<PaymentSignature, PaymentSignatureResponseDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment!.Id));
        CreateMap<PaymentTransactionRequestDto, PaymentTransaction>()
            .BeforeMap((src, dst) =>
            {
                foreach (var property in src.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(src)!;
                        property.SetValue(src, value!.Trim());
                    }
                }
            })
            .AfterMap((src, dst) =>
            {
                foreach (var property in dst.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(dst)!;
                        property.SetValue(dst, value!.Trim());
                    }
                }
            }); ;
        CreateMap<PaymentTransaction, PaymentTransactionResponseDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment!.Id));
       
        CreateMap<ProductImageRequestDto, ProductImage>();
        //.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path!.Trim()));
        CreateMap<ProductImage, ProductImageResponseDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => BaseUrl("product") + src.Path));
        CreateMap<ProductRequestDto, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription.Trim()))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Trim()))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Trim()))
            .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.LongDescription, opt => opt.MapFrom(src => src.ShortDescription.Trim()));
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Brand!.Id))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category!.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.ProductImagesId, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Id).ToList()));
        CreateMap<RoleRequestDto, Role>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Trim()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));
        CreateMap<Role, RoleResponseDto>();
        CreateMap<UserRequestDto, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.ToLower().Trim()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower().Trim()))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Trim()))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Trim()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Trim()))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName.Trim()));
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role!.Id));


    }

    //private List<ProductResponseDto> FromProducts(Product[] products)
    //{
    //    var data = new List<ProductResponseDto>();
    //    foreach (var i in products)
    //    {
    //        data.Add(FromProduct(i));
    //    }
    //    return data;
    //}
    //private ProductResponseDto FromProduct(Product product)
    //{
    //    if (product == null) return null!;
    //    var data = new ProductResponseDto()
    //    {
    //        Id = product.Id,
    //        Code = product.Code,
    //        Name = product.Name,
    //        Color = product.Color,
    //        Size = product.Size,
    //        ShortDescription = product.ShortDescription,
    //        LongDescription = product.LongDescription,
    //        IsActive = product.IsActive,
    //        Price = product.Price,
    //        Purchase = product.Purchase,
    //        Quantity = product.Quantity,
    //        BrandId = product.Brand == null ? 0 : product.Brand.Id,
    //        BrandName = product.Brand == null ? string.Empty : product.Brand.Name,
    //        CategoryId = product.Category == null ? 0 : product.Category.Id,
    //        CategoryName = product.Category == null ? string.Empty : product.Category.Name,
    //        CreatedDate = product.CreatedDate,
    //        ProductImageResponseDtos = FromProductImages(product.ProductImages.ToList()),
    //        UpdatedDate = product.UpdatedDate,
    //    };
    //    return data!;
    //}

    //private List<OrderDetailResponseDto> FromOrderDetails(ICollection<OrderDetail> orderDetails)
    //{
    //    var data = new List<OrderDetailResponseDto>();
    //    foreach (var i in orderDetails)
    //    {
    //        data.Add(FromOrderDetail(i));
    //    }
    //    return data;
    //}
    //private OrderDetailResponseDto FromOrderDetail(OrderDetail orderDetail)
    //{
    //    if (orderDetail == null) return null!;
    //    var data = new OrderDetailResponseDto()
    //    {

    //        Id = orderDetail.Id,
    //        Price = orderDetail.Price,
    //        Quantity = orderDetail.Quantity,
    //        TotalAmount = orderDetail.TotalAmount,
    //        OrderId = orderDetail.Order!.Id,
    //        ProductId = orderDetail.Product!.Id,
    //        ProductCode = orderDetail.Product.Code,
    //        UserResponseDto = FromUser(orderDetail.User!)

    //    };
    //    return data;
    //}

    //private List<PaymentDestinationResponseDto> FromPaymentDestinations(PaymentDestination[] paymentDestinations)
    //{
    //    var data = new List<PaymentDestinationResponseDto>();
    //    foreach (var i in paymentDestinations)
    //    {
    //        data.Add(FromPaymentDestination(i));
    //    }
    //    return data;
    //}
    //private PaymentDestinationResponseDto FromPaymentDestination(PaymentDestination paymentDestination)
    //{
    //    if (paymentDestination == null) return null!;
    //    var data = new PaymentDestinationResponseDto()
    //    {

    //        Id = paymentDestination.Id,
    //        DestinationLogo = paymentDestination.DestinationLogo,
    //        DestinationName = paymentDestination.DestinationName,
    //        DestinationShortName = paymentDestination.DestinationName,
    //        IsActive = paymentDestination.IsActive,
    //        PaymentDestinationParentResponseDto = FromPaymentDestination(paymentDestination.PaymentDestinationParent!)

    //    };
    //    return data!;
    //}

    //private List<PaymentResponseDto> FromPayments(Payment[] payments)
    //{
    //    var data = new List<PaymentResponseDto>();
    //    foreach (var i in payments)
    //    {
    //        data.Add(FromPayment(i));
    //    }
    //    return data;
    //}
    //private PaymentResponseDto FromPayment(Payment payment)
    //{
    //    if (payment == null) return null!;
    //    var data = new PaymentResponseDto()
    //    {

    //        Id = payment.Id,
    //        ExpireDate = payment.ExpireDate,
    //        OrderId = payment.Order!.Id,
    //        PaidAmount = payment.PaidAmount,
    //        PaymentContent = payment.PaymentContent,
    //        PaymentCurrency = payment.PaymentCurrency,
    //        PaymentDate = payment.PaymentDate,
    //        PaymentLanguage = payment.PaymentLanguage,
    //        PaymentLastMessage = payment.PaymentLanguage,
    //        PaymentStatus = payment.PaymentStatus,
    //        RequiredAmount = payment.RequiredAmount,
    //        UserResponseDto = FromUser(payment.User!),
    //        MerchantResponseDto = FromMerchant(payment.Merchant!),
    //        // OrderResponseDto = FromOrder(payment.Order!),
    //        PaymentDestinationResponseDto = FromPaymentDestination(payment.PaymentDestination!)
    //    };
    //    return data;
    //}

    //private MerchantResponseDto FromMerchant(Merchant merchant)
    //{
    //    if (merchant == null) return null!;
    //    var data = new MerchantResponseDto()
    //    {

    //        Id = merchant.Id,
    //        IsActive = merchant.IsActive,
    //        CreateDate = merchant.CreateDate,
    //        MerchantIpnUrl = merchant.MerchantIpnUrl,
    //        MerchantName = merchant.MerchantName,
    //        MerchantReturnUrl = merchant.MerchantReturnUrl,
    //        MerchantWebLink = merchant.MerchantWebLink,
    //        SercetKey = merchant.SercetKey,
    //        UpdateDate = merchant.UpdateDate,
    //        UserResponseDto = FromUser(merchant.User!)

    //    };
    //    return data;
    //}
    //private List<MerchantResponseDto> FromMerchants(Merchant[] merchants)
    //{
    //    var data = new List<MerchantResponseDto>();
    //    foreach (var i in merchants)
    //    {
    //        data.Add(FromMerchant(i));
    //    }
    //    return data;
    //}
    //private RoleResponseDto FromRole(Role role)
    //{
    //    if (role == null) return null!;
    //    var data = new RoleResponseDto()
    //    {

    //        Id = role.Id,
    //        Created = role.Created,
    //        Updated = role.Updated,
    //        Name = role.Name,
    //        Description = role.Description

    //    };
    //    return data;
    //}
    //private List<RoleResponseDto> FromRoles(Role[] roles)
    //{
    //    var data = new List<RoleResponseDto>();
    //    foreach (var i in roles)
    //    {
    //        data.Add(FromRole(i));
    //    }
    //    return data;
    //}
    //private List<OrderResponseDto> FromOrders(Order[] orders)
    //{
    //    var data = new List<OrderResponseDto>();
    //    foreach (var i in orders)
    //    {
    //        data.Add(FromOrder(i));
    //    }
    //    return data;
    //}
    //private OrderResponseDto FromOrder(Order order)
    //{
    //    if (order == null) return null!;
    //    var data = new OrderResponseDto()
    //    {

    //        Id = order.Id,
    //        Address = order.Address,
    //        Amount = order.Amount,
    //        CreateDate = order.CreateDate,
    //        Quantity = order.Quantity,
    //        Status = order.Status,
    //        OrderDetailResponseDtos = FromOrderDetails(order.OrderDetails!.ToList())!,
    //        PaymentId = order.Payment!.Id,
    //        UserResponseDto = FromUser(order.User!)

    //    };
    //    return data;
    //}

    //private List<UserResponseDto> FromUsers(User[] users)
    //{
    //    var data = new List<UserResponseDto>();
    //    foreach (var i in users)
    //    {
    //        data.Add(FromUser(i));
    //    }
    //    return data;
    //}
    //private UserResponseDto FromUser(User user)
    //{
    //    if (user == null) return null!;
    //    var data = new UserResponseDto()
    //    {

    //        Id = user.Id,
    //        Address = user.Address,
    //        CreatedDate = user.CreatedDate,
    //        DateOfBirth = user.DateOfBirth ?? DateTime.Now,
    //        Email = user.Email,
    //        FullName = user.FullName,
    //        Gender = user.Gender,
    //        Image = BaseUrl("user") + user.Image,
    //        IsActive = user.IsActive,
    //        PhoneNumber = user.PhoneNumber,
    //        UpdatedDate = user.UpdatedDate,
    //        Username = user.Username,
    //        RoleResponseDto = FromRole(user.Role!)

    //    };
    //    return data;
    //}
    //private List<ProductImageResponseDto> FromProductImages(List<ProductImage> productImages)
    //{
    //    var data = new List<ProductImageResponseDto?>();
    //    foreach (var item in productImages)
    //    {
    //        data.Add(FromProductImage(item));
    //    }

    //    return data!;
    //}
    //private ProductImageResponseDto FromProductImage(ProductImage productImage)
    //{
    //    if (productImage == null) return null!;
    //    var data = new ProductImageResponseDto()
    //    {
    //        Id = productImage.Id,
    //        Path = productImage.Path,
    //        ProductId = productImage.Product!.Id,
    //        CreatedDate = productImage.CreatedDate,
    //        UpdatedDate = productImage.UpdatedDate
    //    };
    //    return data!;
    //}
    private string BaseUrl(string property)
    {
        if (property.ToLower().Trim() == "product")
        {
            return "https://localhost:7260/Products/";
        }
        else if (property.ToLower().Trim() == "category")
        {
            return "https://localhost:7260/Categories/";
        }
        else if (property.ToLower().Trim() == "brand")
        {
            return "https://localhost:7260/Brands/";
        }
        else if (property.ToLower().Trim() == "user")
        {
            return "https://localhost:7260/Users/";
        }
        return string.Empty;
    }
}


