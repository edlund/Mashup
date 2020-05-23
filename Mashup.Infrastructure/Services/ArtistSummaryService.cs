using Mashup.Core.RestClients;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz;
using Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core;
using Mashup.Domain.Models.Rest.Consumed.Wikidata;
using Mashup.Domain.Models.Rest.Consumed.Wikidata.Core;
using Mashup.Domain.Models.Rest.Consumed.Wikipedia;
using Mashup.Domain.Models.Rest.Consumed.Wikipedia.Core;
using Mashup.Domain.Models.Rest.Produced;
using Mashup.Domain.RestClients.CoverArtArchive;
using Mashup.Domain.RestClients.MusicBrainz;
using Mashup.Domain.RestClients.Wikidata;
using Mashup.Domain.RestClients.Wikipedia;
using Mashup.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mashup.Infrastructure.Services
{
    public class ArtistSummaryService : IArtistSummaryService
    {
        private readonly ICaAlbumRestClient _caAlbumRestClient;
        private readonly IMbArtistRestClient _mbArtistRestClient;
        private readonly IWdWikibaseRestClient _wdWikibaseRestClient;
        private readonly IWpQueryRestClient _wpQueryRestClient;

        public ArtistSummaryService(ICaAlbumRestClient caAlbumRestClient, IMbArtistRestClient mbArtistRestClient,
            IWdWikibaseRestClient wdWikibaseRestClient, IWpQueryRestClient wpQueryRestClient)
        {
            _caAlbumRestClient = caAlbumRestClient;
            _mbArtistRestClient = mbArtistRestClient;
            _wdWikibaseRestClient = wdWikibaseRestClient;
            _wpQueryRestClient = wpQueryRestClient;
        }

        private async Task<RestResponse<WdEntitiesHolder>> GetOneWdEntitiesHolderAsync(MbArtist mbArtist,
            CancellationToken cancellationToken = default)
        {
            var mbArtistWikidataRelation = mbArtist.WikidataRelation;
            if (mbArtistWikidataRelation != null)
            {
                var wdId = new WdId(mbArtistWikidataRelation.Url.Resource.Split("/").Last());
                return await _wdWikibaseRestClient.GetOneEntitiesHolderAsync(wdId, cancellationToken);
            }
            return null;
        }

        private async Task<RestResponse<WpQueryHolder>> GetOneWpQueryHolderAsync(WdEntitiesHolder wdEntitiesHolder,
            CancellationToken cancellationToken = default)
        {
            var wdEntity = wdEntitiesHolder?.Entities?.FirstOrDefault().Value;
            if (wdEntity != null)
            {
                var wpTitles = new WpTitles(wdEntity.EnSiteLink.Title);
                return await _wpQueryRestClient.GetOneQueryHolderAsync(wpTitles, cancellationToken);
            }
            return null;
        }

        public async Task<ArtistSummary> GetOneAsync(MbId mbId, CancellationToken cancellationToken = default)
        {
            var mbArtistResponse = await _mbArtistRestClient.GetOneAsync(mbId, cancellationToken);

            mbArtistResponse.EnsureSuccessStatusCode();

            var caAlbumTasks = mbArtistResponse.Content.ReleaseGroups
                .Select(releaseGroup => _caAlbumRestClient.GetOneAsync(new MbId(releaseGroup.Id)))
                .ToArray();
            
            var wdEntitiesHolderResponse = await GetOneWdEntitiesHolderAsync(mbArtistResponse.Content, cancellationToken);
            var wpQueryHolderResponse = await GetOneWpQueryHolderAsync(wdEntitiesHolderResponse?.Content, cancellationToken);

            Task.WaitAll(caAlbumTasks, cancellationToken);

            var mbReleaseGroupIds = mbArtistResponse.Content.ReleaseGroups
                .Select(releaseGroup => releaseGroup.Id);
            var caImagesUris = caAlbumTasks
                .Select(caAlbumTask => caAlbumTask.Result)
                .Select(caAlbumResponse => caAlbumResponse.Content)
                .Select(caAlbum => caAlbum?.FrontCover?.Image);

            // Zip ReleaseGroup Ids with the Cover Art Image Uris
            // Image requests which resulted in any error are null here, which is fine
            var mbReleaseGroupIdsToCaImageUris = mbReleaseGroupIds
                .Zip(caImagesUris, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);

            return new ArtistSummary
            {
                Id = mbArtistResponse.Content.Id,
                Name = mbArtistResponse.Content.Name,
                Extract = wpQueryHolderResponse?.Content?.Query?.Pages.FirstOrDefault().Value?.Extract,
                Albums = mbArtistResponse.Content.ReleaseGroups
                    .Select(releaseGroup => new AlbumSummary
                    {
                        Id = releaseGroup.Id,
                        Name = releaseGroup.Title,
                        Released = releaseGroup.FirstReleaseDate,
                        CoverArtUri = mbReleaseGroupIdsToCaImageUris.GetValueOrDefault(releaseGroup.Id)
                    })
                    .OrderBy(albumSummary => albumSummary.Released)
                    .ThenBy(albumSummary => albumSummary.Name)
            };
        }
    }
}
