using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Feedback
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public virtual Product? Product { get; set; }
	public virtual User? User { get; set; }
	public int Rating { get; set; }
	public string Comment { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
