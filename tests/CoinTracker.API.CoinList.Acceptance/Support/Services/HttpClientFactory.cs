using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Services
{
    public sealed class HttpClientFactory
    {
        public HttpClient Build()
        {
            var baseAddress = ConfigurationService.Configuration.GetValue("CoinTracker.CoinList:BaseUrl");

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

            return httpClient;
        }
    }
}
