using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Achelous.DomainModeling;
using Achelous.RoutingEngine;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Achelous.Facilities.Command
{
    public class DefaultCommandHandlerRegister : ICommandHandlerRegister
    {
        IDictionary<Type, Type> mapping;
        ICommandRetryStrategy<IResult> retryStrategy;
        ICombineResultStrategy combineStrategy;
        IErrorHandlingStrategy errorHandlingStrategy;
        IKernel kernel;

        public DefaultCommandHandlerRegister(ICommandRetryStrategy<IResult> retryStrategy, ICombineResultStrategy combineStrategy,
                                                IErrorHandlingStrategy errorHandlingStrategy, IKernel kernal)
        {
            mapping = new Dictionary<Type, Type>();
            this.retryStrategy = retryStrategy;
            this.combineStrategy = combineStrategy;
            this.kernel = kernal;
            this.errorHandlingStrategy = errorHandlingStrategy;
        }

        public IResult HandleCommand(ICommand command)
        {
            if (!mapping.ContainsKey(command.GetType()))
                throw new NotSupportedException("The command that you've tried to handle is not registreted!");

            var handler = mapping[command.GetType()];

            if (ReferenceEquals(handler.GetInterface(typeof(ICommandHandler<>).Name, true), null))
                throw new NotSupportedException($"The handler '{handler.GetType().Name}' does not implement correct inteface");

            var handlerInstance = this.kernel.Resolve(handler);

            var methodInfo = handlerInstance.GetType().GetMethod("Handle");

            try
            {
                return retryStrategy.Execute(() => (IResult)methodInfo.Invoke(handlerInstance, new object[] { (ICommand)command }));
            }
            catch (Exception ex)
            {
                this.errorHandlingStrategy.Handle(ex);
                return new CommandResult(false, new List<IEntity>() { new Entity(Guid.Empty, "Error", new Dictionary<string, object>() { { "Error", ex.Message } }) });
            }
        }

        public Task<IResult> HandleCommandAsync(ICommand command)
        {
            return Task.Run(() => HandleCommand(command));
        }

        public IResult HandleMultipleCommands(params ICommand[] commands)
        {
            var results = new List<IResult>();
            foreach (var command in commands)
            {
                results.Add(HandleCommand(command));
            }

            return combineStrategy.CombineResults(results.ToArray());
        }

        public IResult HandleMultipleCommandsParallel(params ICommand[] commands)
        {
            var results = new List<IResult>();
            Parallel.ForEach<ICommand>(commands, (command) =>
            {
                results.Add(HandleCommand(command));
            });

            return combineStrategy.CombineResults(results.ToArray());
        }

        public async Task<IResult> HandleMultipleCommandsAsync(params ICommand[] commands)
        {
            var results = new List<IResult>();
            await Task.Run(() =>
            {
                foreach (var command in commands)
                {
                    results.Add(HandleCommand(command));
                }
            });

            return combineStrategy.CombineResults(results.ToArray());
        }

        public void RegisterHandler(params Assembly[] handlerAssemblies)
        {
            foreach (var assembly in handlerAssemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (ReferenceEquals(type.GetInterface(typeof(ICommandHandler<>).Name), null))
                        continue;

                    var handler = type;
                    var command = handler.GetInterface(typeof(ICommandHandler<>).Name, true).GetGenericArguments()[0];

                    if (!kernel.HasComponent(handler))
                    {
                        kernel.Register(Component.For(handler));
                    }

                    if (!mapping.ContainsKey(command))
                        mapping.Add(command, handler);
                }
            }
        }
    }
}