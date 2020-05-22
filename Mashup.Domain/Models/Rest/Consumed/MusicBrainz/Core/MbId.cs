using Mashup.Core.Models;
using System;

namespace Mashup.Domain.Models.Rest.Consumed.MusicBrainz.Core
{
    public class MbId : StringIdBase
    {
        public MbId(string value) : base(value)
        {
        }

        protected override string Sanitize(string value)
        {
            return Guid.Parse(value.Trim()).ToString();
        }
    }
}
