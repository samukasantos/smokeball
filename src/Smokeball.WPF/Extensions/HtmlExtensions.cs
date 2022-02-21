

using Smokeball.WPF.Application.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Smokeball.WPF.Extensions
{
    public static class HtmlExtensions
    {
        #region Methods

        public static List<ResultDto> ExtractTitleContent(this string content, string pattern)
        {
            try
            {
                var titles = new List<ResultDto>();

                var matches = FindMatches(pattern, content);

                titles = BuildTitles(matches);

                return titles;
            }
            catch
            {
                return null;
            }
        }

        private static List<ResultDto> BuildTitles(MatchCollection matches)
        {
            var items = new List<ResultDto>();

            if (matches.Any())
            {
                foreach (Match match in matches)
                {
                    var value = match.Groups[1].Value;

                    if (!string.IsNullOrEmpty(value))
                    {
                        items.Add(new ResultDto { Title = value });
                    }
                }
            }

            return items;
        }

        private static MatchCollection FindMatches(string pattern, string content)
        {
            if (string.IsNullOrEmpty(pattern)) 
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var regex = new Regex(pattern);

            return regex.Matches(content);
        } 
        
        #endregion
    }
}
