using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.Services
{
    public interface ILocalStorage
    {
        void SaveSet(string display, string value);
        string RetrieveSet(string display);        
    }
}
