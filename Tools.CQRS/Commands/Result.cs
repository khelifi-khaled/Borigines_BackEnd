using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.CQRS.Commands
{
    public class Result : IResult
    {
        public static IResult Success()
        {
            return new Result(true);
        }

        public static IResult Failure(string message)
        {
            return new Result(false, message);
        }

        public bool IsSuccess { get; init; }
        public bool IsFailure { get => !IsSuccess; }
        public string? Message { get; init; }

        private Result(bool isSucces, string? message = null)
        {
            IsSuccess = isSucces;
            Message = message;
        }
    }
}
