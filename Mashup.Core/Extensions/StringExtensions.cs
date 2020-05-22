using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mashup.Core.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> Chunk(this string self, int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Size must be positive");
            }
            for (int i = 0; i < self.Length; i += size)
            {
                var n = self.Length - i;
                yield return self.Substring(i, n < size ? n : size);
            }
        }

        public static IEnumerable<string> SplitCamelCase(this string self)
        {
            // FIXME Maybe numericals should be their own part?
            return Regex.Matches(self, "(^[a-z0-9]+|[A-Z0-9]+(?![a-z])|[A-Z0-9][a-z0-9]+)")
                .OfType<Match>()
                .Select(m => m.Value);
        }
    }
}
