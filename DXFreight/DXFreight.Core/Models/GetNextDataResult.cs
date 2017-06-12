using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Models
{
    public class GetNextDataResult
    {
        [JsonProperty(PropertyName = "Compress")]
        public bool Compress { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "Filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "GUID")]
        public string GUID { get; set; }

        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "MetaType")]
        public int MetaType { get; set; }

        [JsonProperty(PropertyName = "Notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "Priority")]
        public int Priority { get; set; }

        [JsonProperty(PropertyName = "Result")]
        public int Result { get; set; }

        [JsonProperty(PropertyName = "SentDateTime")]
        public string SentDateTime { get; set; }
    }
}
