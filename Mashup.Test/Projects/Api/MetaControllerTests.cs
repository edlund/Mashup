using Mashup.Domain.Models.Rest.Produced;
using Mashup.Test.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace Mashup.Test.Projects.Api
{
    [TestClass]
    public class MetaControllerTests : Blackbox
    {
        [TestMethod]
        public void Test_ErrorStatusCode_GivenNonexistentEndpoint_ResponseIsNotFound()
        {
            var notFoundUri = new Uri("http://localhost/null");
            var notFound = RestClient.GetAsync<object>(notFoundUri).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, notFound.Message.StatusCode);
            Assert.AreEqual(404, notFound.Format.Deserialize<Problem>(notFound.Body).Status);
        }
    }
}
