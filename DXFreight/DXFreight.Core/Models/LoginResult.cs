using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Models
{
    public class LoginResult
    {
        [JsonProperty(PropertyName = "Result")]
        public int Result { get; set; }

        [JsonProperty(PropertyName = "Session")]
        public string Session { get; set; }

        [JsonProperty(PropertyName = "ServerTime")]
        public string ServerTime { get; set; }
    }
}
