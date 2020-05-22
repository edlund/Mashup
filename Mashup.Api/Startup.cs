using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mashup.Core.HttpClients;
using Mashup.Domain.RestClients.CoverArtArchive;
using Mashup.Domain.RestClients.MusicBrainz;
using Mashup.Domain.RestClients.Wikidata;
using Mashup.Domain.RestClients.Wikipedia;
using Mashup.Domain.Services;
using Mashup.Infrastructure.RestClients.CoverArtArchive;
using Mashup.Infrastructure.RestClients.MusicBrainz;
using Mashup.Infrastructure.RestClients.Wikidata;
using Mashup.Infrastructure.RestClients.Wikipedia;
using Mashup.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mashup.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IHttpClientProvider, HttpClientProvider>();

            services.AddScoped<ICaAlbumRestClient, CaAlbumRestClient>();
            services.AddScoped<IMbArtistRestClient, MbArtistRestClient>();
            services.AddScoped<IWdWikibaseRestClient, WdWikibaseRestClient>();
            services.AddScoped<IWpQueryRestClient, WpQueryRestClient>();

            services.AddScoped<IArtistSummaryService, ArtistSummaryService>();
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }

            builder.UseHttpsRedirection();

            builder.UseRouting();

            builder.UseAuthorization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
