namespace API.SDK.Application.Dispatcher;
public interface IQueryHandler<Tquery, TResult> where Tquery : IQuery<TResult>
{
    Task<TResult> Query(Tquery query);
}
