using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tools.CQRS.Commands;
using Tools.CQRS.Queries;

namespace Tools.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlersAndDispatcher(this IServiceCollection container)
        {
            //Ne peut par retourner null car je n'utilise pas de code non managé
            Assembly? assembly = Assembly.GetEntryAssembly()!;

            List<Type> handlerTypes = assembly.GetTypes()
                .Union(assembly.GetReferencedAssemblies().SelectMany(an => Assembly.Load(an).GetTypes()))
                .Where(x => x.GetInterfaces().Any(y => IsHandlerInterface(y)) && x.Name.EndsWith("Handler"))
                .ToList();

            foreach (Type type in handlerTypes)
            {
                Type interfaceType = type.GetInterfaces().Single(y => IsHandlerInterface(y));
                container.AddScoped(interfaceType, type);
            }

            container.AddScoped<IDisptacher, Disptacher>();
            return container;
        }

        private static bool IsHandlerInterface(Type type)
        {
            Type[] cqrsTypes = new[] { typeof(ICommandHandler<>), typeof(IQueryHandler<,>) };

            if (!type.IsGenericType)
                return false;

            Type typeDefinition = type.GetGenericTypeDefinition();
            return cqrsTypes.Contains(typeDefinition);
        }
    }
}
