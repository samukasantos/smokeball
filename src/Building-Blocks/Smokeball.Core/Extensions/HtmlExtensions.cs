

using Smokeball.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Smokeball.Core.Extensions
{
    public static class HtmlExtensions
    {
        #region Methods

        public static List<T> ExtractTitleContent<T>(this string content, string pattern)
            where T : IValue, new()
        {
            try
            {
                var titles = new List<T>();

                var matches = FindMatches(pattern, content);

                titles = BuildTitles<T>(matches);

                return titles;
            }
            catch
            {
                return null;
            }
        }

        private static List<T> BuildTitles<T>(MatchCollection matches)
            where T : IValue, new()
        {
            var items = new List<T>();

            if (matches.Any())
            {
                foreach (Match match in matches)
                {
                    var value = match.Groups[1].Value;

                    if (!string.IsNullOrEmpty(value))
                    {
                        items.Add(new T { Value = value });
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
