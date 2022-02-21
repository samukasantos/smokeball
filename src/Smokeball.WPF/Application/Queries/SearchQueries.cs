
using Microsoft.Extensions.Options;
using Smokeball.WPF.Application.Queries.Base;
using Smokeball.WPF.Application.Queries.Interfaces;
using Smokeball.WPF.Application.Queries.Models;
using Smokeball.WPF.Application.Service.DTO;
using Smokeball.WPF.Extensions;
using Smokeball.WPF.Infra.Http.Integration;
using Smokeball.WPF.Infra.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smokeball.WPF.Application.Queries
{
    public class SearchQueries : QueryBase, ISearchQueries
    {
        #region Fields

        private readonly IHttpConnector httpConnector;
        private readonly RegexPatterns regexPatterns;
        private readonly GoogleHttpIntegration configuration;

        #endregion

        #region Constructor

        public SearchQueries(IHttpConnector httpConnector, IOptions<GoogleHttpIntegration> configuration, IOptions<RegexPatterns> regexPatterns)
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

                var titles = result.ExtractTitleContent(regexPatterns.GoogleH3Pattern);

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
