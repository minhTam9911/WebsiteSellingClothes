using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Merchant
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public string MerchantWebLink { get;set; } = string.Empty;
    public string MerchantIpnUrl { get; set; } = string.Empty;
    public string MerchantReturnUrl { get; set; } = string.Empty;
    public string SercetKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
} 
