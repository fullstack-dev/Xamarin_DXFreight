using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Models
{
    public class GetNextDataInfo
    {
        [JsonProperty(PropertyName = "GetNextDataResult")]
        public GetNextDataResult GetNextDataResult { get; set; }
    }
}
