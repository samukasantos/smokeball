
using Smokeball.WPF.Application.Queries.Base;
using Smokeball.WPF.Application.Service.DTO;
using System.Threading.Tasks;

namespace Smokeball.WPF.Application.Queries.Interfaces
{
    public interface ISearchQueries
    {
        Task<QueryValidationResult> SearchGoogleAsync(SearchDto searchDto);
    }
}
