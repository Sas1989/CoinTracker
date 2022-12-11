using Microsoft.Extensions.Configuration;


namespace CoinTracker.API.CoinList.Acceptance.Support.Services
{
    public sealed class ConfigurationService
    {
        private IConfiguration configuration;
        private static ConfigurationService _istance = new ConfigurationService();

        public static ConfigurationService Configuration
        {
            get { return _istance; }
        }

        private ConfigurationService()
        {
            configuration = LoadConfiguration();
        }

        public string GetValue(string key)
        {
            return configuration[key];
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings-test.json").Build();
        }
    }
}
