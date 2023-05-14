using Tools.CQRS.Commands;
using Tools.CQRS.Queries;

namespace Tools.CQRS
{
    public interface IDisptacher
    {
        IResult Dispatch(ICommand command);
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
