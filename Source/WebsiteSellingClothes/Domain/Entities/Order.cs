using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Order
{
	[Key]
	public string Id { get; set; } = string.Empty;
	public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
	public virtual Payment? Payment { get; set; }
	public string Address { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public decimal Amount { get; set; }
	public string Status { get; set; } = string.Empty;
	public virtual User? User { get; set; }
	public DateTime CreateDate { get; set; }
	public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
	public virtual Discount? Discount { get; set; }
}
