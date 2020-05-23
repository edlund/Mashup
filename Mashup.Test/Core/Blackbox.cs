using Mashup.Api;
using Mashup.Core.HttpClients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Mashup.Test.Core
{
    public class Blackbox
    {
        protected TestServer Server;

        protected HttpClient HttpClient;

        protected JsonTestRestClient RestClient;

        public TestContext TestContext { get; set; }

        public string TestContentRoot => Directory
            .GetParent(Directory.GetCurrentDirectory())
            .Parent
            .Parent
            .FullName;

        [TestInitialize]
        public void TestInitialize()
        {
            Environment.SetEnvironmentVariable(
                "ASPNETCORE_ENVIRONMENT",
                "Development"
            );
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(TestContentRoot);
            Server = new TestServer(webHostBuilder);
            HttpClient = Server.CreateClient();
            RestClient = new JsonTestRestClient(new HttpClientProvider(HttpClient));
            InitializeTestMethod();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupTestMethod();
            HttpClient = null;
            Server = null;
        }

        protected virtual void InitializeTestMethod()
        {
        }

        protected virtual void CleanupTestMethod()
        {
        }

        protected Uri BuildEndpoint(
            string version,
            string controller,
            string action,
            IDictionary<string, string> values)
        {
            var query = string.Join(
                "&",
                from pair in values
                where !string.IsNullOrEmpty(pair.Value)
                select $"{pair.Key}={WebUtility.UrlEncode(pair.Value)}"
            );
            return new Uri($"http://localhost/api/{version}/{controller}/{action}?{query}");
        }
    }
}
