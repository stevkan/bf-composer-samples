using GaiaV2CustomActions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GaiaV2
{
    public class Startup
    {
        private readonly BotSettings m_botSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            m_botSettings = new BotSettings(configuration);
            //var gaiaCertificate = CertificateUtilities.TryLoadPersonalLatestCertificateBySubjectDistinguishedName(m_botSettings.GaiaApplicationCertificateSubjectName);
            //var kuskusDmKustoConnectionStringBuilder = new KustoConnectionStringBuilder(m_botSettings.KuskusDmConnectionString)
            //        .WithAadApplicationCertificateAuthentication(m_botSettings.GaiaAuthorizedAppId, gaiaCertificate, m_botSettings.GaiaAuthorizedAppTenant, sendX5c: true);

            //KustoWebjobsTraceLogger.InitTraceLogger(m_botSettings.ClusterAlias ?? Environment.UserName, kuskusDmKustoConnectionStringBuilder);

            //PrivateTracer.Tracer.TraceInformation($"{m_botSettings.ClusterAlias} is starting");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddControllers().AddNewtonsoftJson();
                services.AddBotRuntime(Configuration);

                var cosmosDbStorageOptions = new CosmosDbPartitionedStorageOptions()
                {
                    CosmosDbEndpoint = "https://common-cosmos-db.documents.azure.com:443/",
                    AuthKey = "825J2uIc5zaxh3YNTU41nTvQzbLDOubbU374y8964UW1oa8itgoLxTqOUfutfkYzBR3wMSLLghn87c5gJ5Hhug==",
                    DatabaseId = "bot-db",
                    ContainerId = "activities"
                };
                IStorage storage = new CosmosDbPartitionedStorage(cosmosDbStorageOptions);
                var conversationState = new ConversationState(storage);
                services.AddSingleton(conversationState);

                var userState = new UserState(storage);
                services.AddSingleton(userState);
            }
            catch(Exception)
            {
                //PrivateTracer.Tracer.TraceError($"Error in Gaia startup");
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();

            // Set up custom content types - associating file extension to MIME type.
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".lu"] = "application/vnd.microsoft.lu";
            
            // Expose static files in manifests folder for skill scenarios.
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       
    }
}
