using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Requests;
public class OrderRequestDto
{
    [Required(ErrorMessage ="The carts id is required")]
    public int[]? CartIds { get; set; }
    [Required(ErrorMessage = "The address is required")]
    [MaxLength(256, ErrorMessage = "The address must be a maximum of 256 characters in length")]
    public string Address { get; set; } = string.Empty;
    public string? DiscountId { get; set; } = string.Empty;
    public string PaymentDestinationId { get; set; } = "DEST001";
    public string PaymentCurrency { get; set; } = "VND";
    public string MerchantId { get; set; } = "MER001";
    public string PaymentLanguage { get; set; } = "vn";
    public string Note { get; set; } = string.Empty;
}

