﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs;
public class RefreshTokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}
