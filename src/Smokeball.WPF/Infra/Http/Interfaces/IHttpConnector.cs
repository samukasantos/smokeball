

using System.Threading.Tasks;

namespace Smokeball.WPF.Infra.Http.Interfaces
{
    public interface IHttpConnector
    {
        public string BaseUri { get; set; }
        Task<string> GetListAsync(string resourceUri, string query = null);
    }
}
