using API.SDK.Application.Dispatcher;
using API.Wallets.Application;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace API.Wallets.Infrastructure.Services;

public class CqrsDispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CqrsDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await handler.Handle(command);
    }

    public async Task<TResult?> Send<TResult>(ICommand<TResult> command)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        var handlerMethod = handler.GetType().GetMethod(nameof(IRequestHandler<ICommand<TResult>, TResult>.Handle));

        var result = handlerMethod?.Invoke(handler, new object[] { command });

        if(result is null)
        {
            return default;
        }

        return await (Task<TResult?>)result;

    }

    public async Task<TResult?> Send<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        var handlerMethod = handler.GetType().GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.Query));

        var result = handlerMethod?.Invoke(handler, new object[] { query });

        if (result is null)
        {
            return default;
        }

        return await (Task<TResult?>)result;
    }
}
