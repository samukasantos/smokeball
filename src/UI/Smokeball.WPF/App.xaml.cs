using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smokeball.Application.Queries;
using Smokeball.Application.Queries.Interfaces;
using Smokeball.Application.Service;
using Smokeball.Application.Service.Configuration;
using Smokeball.Application.Service.Interfaces;
using Smokeball.Infra.Http;
using Smokeball.Infra.Http.Interfaces;
using Smokeball.Infra.IoC;
using Smokeball.WPF.Presentation.View;
using Smokeball.WPF.Presentation.View.Interfaces;
using Smokeball.WPF.Presentation.ViewModel;
using System;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace Smokeball.WPF
{
    public partial class App : System.Windows.Application, IServiceCollectionManager
    {
        #region Properties

        protected IConfiguration configuration;
        protected IServiceProvider serviceProvider;

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();

            serviceProvider = ConfigureServices(configuration, new ServiceCollection());

            MainWindow = (Window)serviceProvider.GetRequiredService<IMainView>();

            MainWindow.Show();
        }

        public IServiceProvider ConfigureServices(IConfiguration configuration, ServiceCollection services = null)
        {
            //Options
            services.Configure<GoogleHttpIntegration>(options => configuration.GetSection("GoogleHttpIntegration").Bind(options));
            services.Configure<GoogleRegexPatterns>(options => configuration.GetSection("GoogleRegexPatterns").Bind(options));

            //Http
            services.AddHttpClient();
            services.AddScoped<IHttpConnector>(provider =>
            {
                var httpFactory = provider.GetRequiredService<IHttpClientFactory>();

                var httpConnector = new HttpConnector(httpFactory) { BaseUri = configuration["GoogleHttpIntegration:BaseUri"] };

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
