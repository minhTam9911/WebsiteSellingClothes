﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses;
public record ServiceContainerResponseDto(int Status,bool Flag, string Message = null!);
