
namespace CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions.Interface
{
    public interface IApiActionBase
    {
        Task Clean();
        Task Delete(Guid id);
    }
}