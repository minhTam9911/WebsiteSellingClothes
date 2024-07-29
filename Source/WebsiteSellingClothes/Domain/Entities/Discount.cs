using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Discount
{
	[Key]
	public string Id { get; set; } = string.Empty;
	public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
	public decimal Percentage { get; set; }
	public int Quantity { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
