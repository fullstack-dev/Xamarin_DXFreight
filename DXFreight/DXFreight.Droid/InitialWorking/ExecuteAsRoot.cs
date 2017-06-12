using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXFreight.Core.InitialWorking;
using Java.Lang;

namespace DXFreight.Droid.InitialWorking
{
    public class ExecuteAsRoot : IExecuteAsRoot
    {
        public bool CanRunRootCommands()
        {
            bool retval = false;
            Process suProcess;
            try
            {
                suProcess = Runtime.GetRuntime().Exec("su");
                var os = new Java.IO.DataOutputStream(suProcess.OutputStream);
                var osRes = new Java.IO.DataInputStream(suProcess.InputStream);
                if (null != os && null != osRes)
                {
                    os.WriteBytes("id\n");
                    os.Flush();
                    string currUid = osRes.ReadLine();
                    bool exitSu = false;
                    if (null == currUid)
                    {
                        retval = false;
                        exitSu = false;
                        //Console.WriteLine("Can't get root access or denied by user");
                    }
                    else if (true == currUid.Contains("uid=0"))
                    {
                        retval = true;
                        exitSu = true;
                        //Console.WriteLine("Root access granted");
                    }
                    else
                    {
                        retval = false;
                        exitSu = true;
                        //Console.WriteLine("Root access rejected: " + currUid);
                    }
                    if (exitSu)
                    {
                        os.WriteBytes("exit\n");
                        os.Flush();
                    }
                }
            }
            catch (Java.Lang.Exception e)
            {
                retval = false;
                //Console.WriteLine("Root access rejected [" + e.Class.Name + "] : " + e.Message);
            }
            return retval;
        }
    }
}
