using API.SDK.Application.Dispatcher;

namespace API.Wallets.Application;

public interface IDispatcher
{
    Task<TResult?> Send<TResult>(ICommand<TResult> command);
    Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    Task<TResult?> Send<TResult>(IQuery<TResult> query);
}
