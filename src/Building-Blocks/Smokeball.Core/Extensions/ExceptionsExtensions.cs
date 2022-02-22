using System;
using System.Collections.Generic;
using System.Linq;

namespace Smokeball.Core.Extensions
{
    public static class ExceptionsExtensions
    {
        #region Methods

        private static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source,
          Func<TSource, TSource> nextItem,
          Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        private static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static string GetAllMessages(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException)
                .Select(ex => ex.Message);

            return string.Join(Environment.NewLine, messages);
        }

        #endregion
    }
}
