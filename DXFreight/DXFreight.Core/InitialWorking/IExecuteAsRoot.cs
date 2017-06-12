using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.InitialWorking
{
    public interface IExecuteAsRoot
    {
        bool CanRunRootCommands();
    }
}
