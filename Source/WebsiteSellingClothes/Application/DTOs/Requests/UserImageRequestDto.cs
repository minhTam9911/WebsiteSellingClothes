using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests;
public class UserImageRequestDto
{
    [Required(ErrorMessage ="The image is required")]
    public IFormFile? Image { get; set; }
}
