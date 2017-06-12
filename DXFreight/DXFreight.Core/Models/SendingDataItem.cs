using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Models
{
    public class SendingDataItem
    {
        public string customer { get; set; }
        public string session { get; set; }
        public int metatype { get; set; }
        public string filename { get; set; }
        public string notes { get; set; }
        public string datetime { get; set; }
        public int priority { get; set; }
    }
}
