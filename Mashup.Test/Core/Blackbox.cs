using Mashup.Api;
using Mashup.Core.HttpClients;
using Mashup.Core.RestClients;
using Mashup.Core.RestClients.Formats;
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
    /// <summary>
    /// Base class for blackbox/integration tests.
    /// </summary>
    /// <remarks>
    /// You could argue that test classes should have a blackbox,
    /// not that they are a blackbox, and I wouldn't disagree.
    /// 
    /// However, is-a, instead of has-a, allows us to easily
    /// manage initialization and cleanup. It's an okay trade-off
    /// regarding sub-optimal abstraction vs. convenience.
    /// 
    /// If we were working with C++, we would've had the opportunity
    /// to use the elusive language feature "private inheritance",
    /// implemented-in-terms-of.
    /// </remarks>
    public abstract class Blackbox
    {
        public class JsonRestClient : RestClient<Json>
        {
            public JsonRestClient(IHttpClientProvider httpClientProvider) : base(httpClientProvider)
            {
            }
        }

        protected TestServer Server;

        protected HttpClient HttpClient;

        protected JsonRestClient RestClient;

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
            RestClient = new JsonRestClient(new HttpClientProvider(HttpClient));
            InitializeTestMethod();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupTestMethod();
            RestClient = null;
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
