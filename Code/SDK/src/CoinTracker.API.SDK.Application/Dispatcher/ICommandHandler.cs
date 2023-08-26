namespace API.SDK.Application.Dispatcher;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}

public interface IRequestHandler<TCommand, TResult> where TCommand: ICommand<TResult>
{
    Task<TResult> Handle(TCommand command);
}