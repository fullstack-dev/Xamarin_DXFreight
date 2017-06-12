using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DXFreight.Core.Services;
using DXFreight.Droid.Views;

namespace DXFreight.Droid.Services
{
    public class ChangeBase64XML : IChangeBase64XML
    {
        public string GetBase64ToXML(string base64String)
        {
            byte[] stringAsBytes = Android.Util.Base64.Decode(base64String, Android.Util.Base64Flags.Default);
            string m_XMLStr = System.Text.Encoding.ASCII.GetString(stringAsBytes);

            return m_XMLStr;
        }
    }
}