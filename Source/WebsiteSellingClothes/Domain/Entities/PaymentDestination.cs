using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class PaymentDestination
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string DestinationLogo { get; set; } = string.Empty;
    public string DestinationShortName { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public virtual PaymentDestination? PaymentDestinationParent { get; set; }
}
