using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using ReadingIsGood.API;
using System;
using System.IO;
using System.Net.Http;

namespace ReadingIsGood.Api.IntegrationTest
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient HttpClient { get; set; }

        public TestClientProvider()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.Development.json");

            _testServer = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((context, conf) =>
            {
                conf.AddJsonFile(configPath);
            }).UseStartup<Startup>());

            HttpClient = _testServer.CreateClient();
        }

        public void Dispose()
        {
            _testServer?.Dispose();
            HttpClient?.Dispose();
        }
    }
}
