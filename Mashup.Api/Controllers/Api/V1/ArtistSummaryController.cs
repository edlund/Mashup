using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using Mashup.Domain.Models.Rest.Produced;
using Mashup.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mashup.Api.Controllers.Api.V1
{
    /// <summary>
    /// Artist Summaries.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArtistSummaryController : ApiControllerBase
    {
        private readonly ILogger<ArtistSummaryController> _logger;
        private readonly IArtistSummaryService _artistSummaryService;

        public ArtistSummaryController(ILogger<ArtistSummaryController> logger, IArtistSummaryService artistSummaryService)
        {
            _logger = logger;
            _artistSummaryService = artistSummaryService;
        }

        /// <summary>
        /// Get One `ArtistSummary` from a given `MBID`.
        /// </summary>
        /// <param name="mbId">The MusicBrainzID.</param>
        /// <returns>The requested `ArtistSummary`.</returns>
        [HttpGet("getone")]
        public async Task<ArtistSummary> GetOne(
            [FromQuery] string mbId)
        {
            return await _artistSummaryService.GetOneAsync(new MbId(mbId), default);
        }
    }
}
