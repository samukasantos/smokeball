
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smokeball.WPF.Application.Queries;
using Smokeball.WPF.Application.Queries.Interfaces;
using Smokeball.WPF.Application.Queries.Models;
using Smokeball.WPF.Application.Service;
using Smokeball.WPF.Application.Service.Interfaces;
using Smokeball.WPF.Infra.Http;
using Smokeball.WPF.Infra.Http.Integration;
using Smokeball.WPF.Infra.Http.Interfaces;
using Smokeball.WPF.Presentation.View;
using Smokeball.WPF.Presentation.View.Interfaces;
using Smokeball.WPF.Presentation.ViewModel;
using System;
using System.Net.Http;

namespace Smokeball.WPF.Infra.IoC
{
    public static class ServiceCollectionExtensions 
    {
        #region Methods

        public static IServiceProvider ConfigureServices(this ServiceCollection services, IConfiguration configuration) 
        {
            //Options
            services.Configure<GoogleHttpIntegration>(options => configuration.GetSection("GoogleHttpIntegration").Bind(options));
            services.Configure<RegexPatterns>(options => configuration.GetSection("RegexPatterns").Bind(options));
            
            //Http
            services.AddHttpClient();
            services.AddScoped<IHttpConnector>(provider =>
            {
                var httpFactory = provider.GetRequiredService<IHttpClientFactory>();

                var httpConnector = new HttpConnector(httpFactory){  BaseUri = configuration["GoogleHttpIntegration:BaseUri"] };

                httpConnector.SetMaxRetryAttempts(int.Parse(configuration["HttpConfiguration:MaxRetryAttempts"]));

                return httpConnector;
            });

            //Views
            services.AddScoped<IMainView, MainView>();

            //ViewModels
            services.AddScoped<MainViewModel>();

            //Application
            services.AddScoped<ISearchApplicationService, SearchApplicationService>();

            //Queries
            services.AddScoped<ISearchQueries, SearchQueries>();

            return services.BuildServiceProvider();
        }

        #endregion
    }
}
