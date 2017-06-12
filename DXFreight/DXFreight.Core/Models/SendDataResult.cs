using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Models
{
    public class SendDataResult
    {
        [JsonProperty(PropertyName = "DataPending")]
        public int DataPending { get; set; }

        [JsonProperty(PropertyName = "GUID")]
        public string GUID { get; set; }

        [JsonProperty(PropertyName = "MessagePending")]
        public int MessagePending { get; set; }

        [JsonProperty(PropertyName = "Result")]
        public int Result { get; set; }
    }
}
