using System;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Util;
using Microsoft.Extensions.DependencyInjection;
using Piranha.Lucene.Search.Actors;
using Piranha.Lucene.Search.Options;
using Piranha.Lucene.Search.Services;
using Proto;

namespace Piranha.Lucene.Search
{
    public static class PiranhaSearchExtensions
    {
        /// <summary>
        ///     Adds the Lucene Search module.
        /// </summary>
        /// <param name="serviceBuilder">The service builder</param>
        /// <returns>The services</returns>
        public static PiranhaServiceBuilder UseLuceneSearch(this PiranhaServiceBuilder serviceBuilder,
            Action<LuceneOptions> configure)
        {
            serviceBuilder.Services.AddPiranhaLuceneSearch(configure);

            return serviceBuilder;
        }

        /// <summary>
        ///     Adds the Lucene Search module.
        /// </summary>
        /// <param name="services">The current service collection</param>
        /// <returns>The services</returns>
        public static IServiceCollection AddPiranhaLuceneSearch(this IServiceCollection services)
        {
            return services.AddPiranhaLuceneSearch(opt =>
            {
                const LuceneVersion appLuceneVersion = LuceneVersion.LUCENE_48;
                opt.Version = appLuceneVersion;

                var basePath = Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData);
                opt.IndexPath = Path.Combine(basePath, "index");

                var analyzer = new StandardAnalyzer(appLuceneVersion);
                opt.Analyzer = analyzer;

                var indexConfig = new IndexWriterConfig(appLuceneVersion, analyzer);
                opt.IndexConfig = indexConfig;
            });
        }

        public static IServiceCollection AddPiranhaLuceneSearch(
            this IServiceCollection services,
            Action<LuceneOptions> configure)
        {
            App.Modules.Register<Module>();

            services.Configure(configure);

            var system = new ActorSystem();
            var props = Props.FromProducer(() => new LuceneActor());
            var pid = system.Root.Spawn(props);
            var address = new LuceneActorAddress(system, pid);
            services.AddSingleton<ILuceneActorAddress>(address);

            // Register the search service

            services.AddSingleton<ISearch, LuceneSearchService>();

            return services;
        }
    }
}