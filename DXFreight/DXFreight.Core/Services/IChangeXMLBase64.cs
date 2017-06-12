using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Services
{
    public interface IChangeXMLBase64
    {
        string GetDeviceXMLToBase64();
        string GetLoginXMLToBase64();
    }
}
