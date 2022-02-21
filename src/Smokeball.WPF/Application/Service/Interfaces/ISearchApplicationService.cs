
using Smokeball.WPF.Presentation.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smokeball.WPF.Application.Service.Interfaces
{
    public interface ISearchApplicationService
    {
        Task<ResultViewModel> SearchAsync(SearchViewModel searchViewModel);
    }
}
