using Mashup.Domain.Models.Rest.Produced;
using Mashup.Test.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void Test_GetOne_GivenValidMbId_ResponseIsArtistSummary()
        {
            var artistSummaryUri = BuildArtistSummaryGetOneEndpoint("5b11f4ce-a62d-471e-81fc-a69a8278c7da");
            var artistSummary = RestClient.GetAsync<ArtistSummary>(artistSummaryUri).Result;
            artistSummary.EnsureSuccessStatusCode();
            Assert.AreEqual("Nirvana", artistSummary.Content.Name);
            Assert.AreEqual("Bleach", artistSummary.Content.Albums.First().Name);
            Assert.IsNotNull(artistSummary.Content.Albums.First().CoverArtUri);
        }
    }
}
