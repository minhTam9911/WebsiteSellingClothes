using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class PaymentSignature
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string SignatureValue { get; set; } = string.Empty;
    //Mã hóa chữa ký dạng nào
    public string SignatureAlgo { get; set; } = string.Empty;
    public DateTime SignatureDate { get; set; }
    //Chữ ký của bên nào
    public string SignatureOwn { get; set; } = string.Empty;
    public virtual Payment? Payment { get; set; }
}
