
using Tools.CQRS.Commands;
using Tools.CQRS.Queries;

namespace Tools.CQRS
{
    public class Disptacher : IDisptacher
    {
        private readonly IServiceProvider _serviceProvider;

        public Disptacher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IResult Dispatch(ICommand command)
        {
            Type commandHandlerType = typeof(ICommandHandler<>);
            Type concreteCommandHandlerType = commandHandlerType.MakeGenericType(command.GetType());

            dynamic? handler = _serviceProvider.GetService(concreteCommandHandlerType);

            if (handler is null)
            {
                throw new InvalidOperationException($"the type {concreteCommandHandlerType.FullName} is'nt registered");
            }

            return handler.Execute((dynamic)command);
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            Type queryHandlerType = typeof(IQueryHandler<,>);
            Type concreteQueryHandlerType = queryHandlerType.MakeGenericType(query.GetType(), typeof(TResult));

            dynamic? handler = _serviceProvider.GetService(concreteQueryHandlerType);

            if (handler is null)
            {
                throw new InvalidOperationException($"the type {concreteQueryHandlerType.FullName} is'nt registered");
            }

            return handler.Execute((dynamic)query);
        }
    }
}
