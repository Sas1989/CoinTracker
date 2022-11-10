using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Builders;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace CoinTracker.API.CoinList.Acceptance.Hooks
{
    [Binding]
    public static class DockerComposeHooks
    {
        private static ICompositeService containerService;


        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var dockerFile = ConfigurationService.Configuration.GetValue("DockerComposeFileName");
            var baseAddress = ConfigurationService.Configuration.GetValue("CoinTracker.CoinList:BaseUrl");
            var dockerComposePath = GetDockerComposeLocation(dockerFile);
            containerService = new Builder()
                                .UseContainer()
                                .WithName("CoinTracker.FunctionalTest")
                                .UseCompose()
                                .FromFile(dockerComposePath)
                                .RemoveOrphans().ForceBuild()
                                .WaitForHttp("cointracker.api.coinlist.test", $"{baseAddress}/api/coin", 30000)
                                .Build();
            containerService.Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            containerService.Stop();
            containerService.Dispose();
        }

        private static string GetDockerComposeLocation(string dockerFile)
        {
            var directory = Directory.GetCurrentDirectory();


            while (!Directory.EnumerateFiles(directory, "*.yml").Any(f => f.EndsWith(dockerFile)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }

            return Path.Combine(directory, dockerFile);

        }
    }
}
