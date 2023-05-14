using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.CQRS.Commands
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        string? Message { get; }
    }
}
