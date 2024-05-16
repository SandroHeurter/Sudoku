using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Utils
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Descendants<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> descendBy)
        {
            foreach (var value in source)
            {
                yield return value;

                foreach (var child in descendBy(value).Descendants(descendBy))
                {
                    yield return child;
                }
            }
        }
    }
}