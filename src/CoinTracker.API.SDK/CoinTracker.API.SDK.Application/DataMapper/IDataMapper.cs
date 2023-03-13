namespace CoinTracker.API.SDK.Application.DataMapper
{
    public interface IDataMapper
    {
        TDestination Map<TDestination>(object sourceObj);
    }
}
