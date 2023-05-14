using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tools.JWT.Interfaces
{
    public interface IToken
    {
        string GenerateToken(params Claim[] claims);
    }
}
