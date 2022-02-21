using Smokeball.WPF.Infra.Http.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smokeball.WPF.Infra.Http
{
    public class HttpConnector : IHttpConnector
    {
        #region Fields

        private readonly IHttpClientFactory httpFactory;

        #endregion

        #region Properties

        public string BaseUri { get; set; }

        #endregion

        #region Constructor

        public HttpConnector(IHttpClientFactory httpFactory)
        {
            this.httpFactory = httpFactory;
        }

        #endregion

        #region Methods

        public async Task<string> GetListAsync(string resourceUri, string query = null)
        {
            using (var httpclient = httpFactory.CreateClient())
            {
                var resource = CreateResourceUri(resourceUri, query);

                using (var response = await httpclient.GetAsync(resource))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        private Uri CreateResourceUri(string resourceUri, string query = null) 
        {
            if (string.IsNullOrEmpty(resourceUri)) 
            {
                throw new ArgumentNullException(nameof(resourceUri));
            }

            if (string.IsNullOrEmpty(query))
            {
                return new Uri($"{BaseUri}{resourceUri}");
            }
            else
            {
                return new Uri($"{BaseUri}{resourceUri}?{query}");
            }
        }

        #endregion
    }
}
