using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Product
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public string Color { get; set; } = string.Empty;
	public string Size { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public string Description { get; set; } = string.Empty;
	public bool IsActive { get; set; }
	public int Purchase { get; set; }
	public virtual Brand? Brand { get; set; }
	public virtual Category? Category { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
