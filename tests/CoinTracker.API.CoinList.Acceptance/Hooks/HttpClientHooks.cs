using BoDi;
using CoinTracker.API.CoinList.Acceptance.Support.Services;

namespace CoinTracker.API.CoinList.Acceptance.Hooks
{
    [Binding]
    public sealed class HttpClientHooks
    {
        private IObjectContainer objectContainer;

        public HttpClientHooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void AddHttpClient()
        {
            var baseAddress = ConfigurationService.Configuration.GetValue("CoinTracker.CoinList:BaseUrl");

            var httpClient = new HttpClientFactory().Build();
            objectContainer.RegisterInstanceAs(httpClient);
        }

    }
}