using Microsoft.Extensions.Options;
using Smokeball.Application.Queries.Interfaces;
using Smokeball.Application.Service.Configuration;
using Smokeball.Application.Service.DTO;
using Smokeball.Core.Application.Queries;
using Smokeball.Core.Extensions;
using Smokeball.Infra.Http.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Smokeball.Application.Queries
{
    public class SearchQueries : QueryBase, ISearchQueries
    {
        #region Fields

        private readonly IHttpConnector httpConnector;
        private readonly GoogleRegexPatterns regexPatterns;
        private readonly GoogleHttpIntegration configuration;

        #endregion

        #region Constructor

        public SearchQueries(IHttpConnector httpConnector,
            IOptions<GoogleHttpIntegration> configuration,
            IOptions<GoogleRegexPatterns> regexPatterns)
        {
            this.httpConnector = httpConnector;
            this.configuration = configuration.Value;
            this.regexPatterns = regexPatterns.Value;
        }

        #endregion

        #region Methods

        public async Task<QueryValidationResult> SearchGoogleAsync(SearchDto searchDto)
        {
            try
            {
                var result = await httpConnector.GetListAsync(configuration.Search,
                        $"num={configuration.SearchOcurrency}&q={searchDto.Keyword.ConvertToQueryParameter()}");

                var titles = result.ExtractTitleContent<ResultDto>(regexPatterns.GoogleH3Pattern);

                if (titles.Any())
                {
                    ValidationResult.Data = titles.Take(configuration.SearchOcurrency).ToList();
                }
            }
            catch (Exception e)
            {
                AddError(e.GetAllMessages());
            }

            return ValidationResult;
        }

        #endregion
    }
}
