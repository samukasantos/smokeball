

using Smokeball.Application.Service.DTO;
using Smokeball.Core.Application.Queries;
using System.Threading.Tasks;

namespace Smokeball.Application.Queries.Interfaces
{
    public interface ISearchQueries
    {
        Task<QueryValidationResult> SearchGoogleAsync(SearchDto searchDto);
    }
}
