using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons.Exceptions;
public class JwtTokenInvalidException : Exception
{
    public JwtTokenInvalidException(string message) : base(message)
    {
    }
}