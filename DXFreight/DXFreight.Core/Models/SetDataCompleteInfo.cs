using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Models
{
    public class SetDataCompleteInfo
    {
        [JsonProperty(PropertyName = "SetDataCompleteResult")]
        public SetDataCompleteResult SetDataCompleteResult { get; set; }
    }
}
