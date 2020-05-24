using Mashup.Domain.Models.Rest.Produced;
using Mashup.Test.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Mashup.Test.Projects.Api.V1
{
    [TestClass]
    public class ArtistSummaryControllerTests : Blackbox
    {
        protected Uri BuildArtistSummaryGetOneEndpoint(string mbId)
        {
            return BuildEndpoint(
                "v1",
                "artistsummary",
                "getone",
                new Dictionary<string, string>
                {
                    { "mbId", mbId }
                });
        }

        [TestMethod]
        public void Test_GetOne_GivenValidMbId_ResponseIsOk()
        {
            var artistSummaryUri = BuildArtistSummaryGetOneEndpoint("5b11f4ce-a62d-471e-81fc-a69a8278c7da");
            var artistSummary = RestClient.GetAsync<ArtistSummary>(artistSummaryUri).Result;
            artistSummary.EnsureSuccessStatusCode();
            Assert.AreEqual("Nirvana", artistSummary.Content.Name);
            Assert.AreEqual("Bleach", artistSummary.Content.Albums.First().Name);
            Assert.IsNotNull(artistSummary.Content.Albums.First().CoverArtUri);
        }

        [TestMethod]
        public void Test_GetOne_GivenInvalidMbId_ResponseIsBadRequest()
        {
            var artistSummaryUri = BuildArtistSummaryGetOneEndpoint("badcafe");
            var artistSummary = RestClient.GetAsync<ArtistSummary>(artistSummaryUri).Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, artistSummary.Message.StatusCode);
        }

        [TestMethod]
        public void Test_GetOne_GivenNonexistentMbId_ResponseIsNotFound()
        {
            // The ancient, evil curse of Zargothrax messed up a
            // couple of bits here...
            var artistSummaryUri = BuildArtistSummaryGetOneEndpoint("85997032-ff48-4383-b86a-ffffffffffff");
            var artistSummary = RestClient.GetAsync<ArtistSummary>(artistSummaryUri).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, artistSummary.Message.StatusCode);
        }
    }
}
