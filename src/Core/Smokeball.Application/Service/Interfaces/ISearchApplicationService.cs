
using Smokeball.Application.Service.Models;
using System.Threading.Tasks;

namespace Smokeball.Application.Service.Interfaces
{
    public interface ISearchApplicationService
    {
        Task<ResultModel> SearchAsync(SearchModel searchModel);
    }
}
