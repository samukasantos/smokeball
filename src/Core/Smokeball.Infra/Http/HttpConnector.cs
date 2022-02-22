
using Polly;
using Polly.Retry;
using Smokeball.Infra.Http.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smokeball.Infra.Http
{
    public class HttpConnector : IHttpConnector
    {
        #region Fields

        private readonly IHttpClientFactory httpFactory;
        private AsyncRetryPolicy retryPolicy;

        #endregion

        #region Properties

        public string BaseUri { get; set; }
        public int MaxRetryAttempts { get; private set; } = 3;

        #endregion

        #region Constructor

        public HttpConnector(IHttpClientFactory httpFactory)
        {
            this.httpFactory = httpFactory;

            CreateRetryPolice();
        }

        #endregion

        #region Methods

        public void SetMaxRetryAttempts(int maxRetryAttempts)
        {
            if (maxRetryAttempts > 0)
            {
                MaxRetryAttempts = maxRetryAttempts;

                CreateRetryPolice();
            }
        }

        private void CreateRetryPolice()
        {
            retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(MaxRetryAttempts, i => TimeSpan.FromSeconds(2));
        }

        public async Task<string> GetListAsync(string resourceUri, string query = null)
        {
            var resource = CreateResourceUri(resourceUri, query);

            return await retryPolicy.ExecuteAsync(async () =>
            {
                using (var httpclient = httpFactory.CreateClient())
                {
                    using (var response = await httpclient.GetAsync(resource))
                    {
                        response.EnsureSuccessStatusCode();

                        return await response.Content.ReadAsStringAsync();
                    }
                }
            });
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
