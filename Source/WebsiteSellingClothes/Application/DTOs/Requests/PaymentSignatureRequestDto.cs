using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class PaymentSignatureRequestDto
{
    public string SignatureValue { get; set; } = string.Empty;
    public string SignatureAlgo { get; set; } = string.Empty;
    public DateTime SignatureDate { get; set; }
    public string SignatureOwn { get; set; } = string.Empty;
    public string PaymentId { get; set; } = string.Empty;
}
