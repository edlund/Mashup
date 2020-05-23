using Mashup.Core.Ids;
using System;
using System.Text.RegularExpressions;

namespace Mashup.Domain.Models.Rest.Consumed.Wikidata.Core
{
    public class WdId : StringIdBase
    {
        public WdId(string value) : base(value)
        {
        }

        protected override string Sanitize(string value)
        {
            if (!Regex.Match(value, @"^[a-zA-Z0-9]+$").Success)
            {
                throw new FormatException($"The given Id \"{value}\" does not look like a Wikidata Id");
            }
            return value;
        }
    }
}
