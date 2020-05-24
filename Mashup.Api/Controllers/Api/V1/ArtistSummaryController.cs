using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using Mashup.Domain.Models.Rest.Produced;
using Mashup.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Mashup.Api.Controllers.Api.V1
{
    /// <summary>
    /// Artist Summaries.
    /// </summary>
    [Produces("application/json")]
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
        /// <response code="200">Returns the `ArtistSummary`.</response>
        /// <response code="400">If the given `mbId` is invalid.</response>
        /// <response code="404">If the MusicBrainz Artist does not exist.</response>
        /// <response code="503">If the MusicBrainz REST API imposed a rate limit.</response>
        [HttpGet("getone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ArtistSummary> GetOne(
            [FromQuery] string mbId)
        {
            return await _artistSummaryService.GetOneAsync(new MbId(mbId), default);
        }
    }
}
