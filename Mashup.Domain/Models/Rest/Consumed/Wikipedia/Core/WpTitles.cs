using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mashup.Domain.Models.Rest.Consumed.Wikipedia.Core
{
    public class WpTitles
    {
        public string Value { get; set; }

        public WpTitles(string title)
        {
            Value = HttpUtility.UrlEncode(title);
        }

        public WpTitles(IEnumerable<string> titles)
        {
            Value = string.Join("|", titles.Select(HttpUtility.UrlEncode));
        }
    }
}
