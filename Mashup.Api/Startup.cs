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
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;

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
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mashup",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

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
                builder.UseExceptionHandler("/error-development");
            }
            else
            {
                // Postman does not work well self-signed certificates.
                // Only require HTTPS in test/stage/production where we need
                // at least "Let's Encrypt" anyway.
                builder.UseHttpsRedirection();
                builder.UseExceptionHandler("/error");
            }
            builder.UseStatusCodePagesWithReExecute("/error/{0}");

            builder.UseRouting();

            builder.UseAuthorization();
            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mashup API V1");
            });

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
