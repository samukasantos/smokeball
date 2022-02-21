using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smokeball.WPF.Infra.IoC;
using Smokeball.WPF.Presentation.View.Interfaces;
using System;
using System.IO;
using System.Windows;

namespace Smokeball.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
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

            serviceProvider = new ServiceCollection().ConfigureServices(configuration);

            MainWindow = (Window)serviceProvider.GetRequiredService<IMainView>();

            MainWindow.Show();
        }

        #endregion

    }
}
