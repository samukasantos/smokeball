using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Smokeball.Infra.IoC
{
    public interface IServiceCollectionManager
    {
        #region Methods

        public IServiceProvider ConfigureServices(IConfiguration configuration, ServiceCollection services = null);

        #endregion
    }
}
