using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class OrderDetail
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public virtual Order? Order { get; set; }
	public virtual Product? Product {get;set;}
	public string Address { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public decimal Price { get; set; }
	public decimal TotalAmount { get; set; }

}
