using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public class ErrorValidationResponse
{

    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Errors { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;

}
