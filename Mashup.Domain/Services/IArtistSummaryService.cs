using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using Mashup.Domain.Models.Rest.Produced;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Domain.Services
{
    public interface IArtistSummaryService
    {
        public Task<ArtistSummary> GetOneAsync(MbId mbId, CancellationToken cancellationToken = default);
    }
}
