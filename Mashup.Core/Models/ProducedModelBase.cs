using System;
using System.Collections.Generic;
using System.Text;

namespace Mashup.Core.Models
{
    public class ProducedModelBase
    {
        public string ClassName => GetType().Name.Split('`')[0];
    }
}
