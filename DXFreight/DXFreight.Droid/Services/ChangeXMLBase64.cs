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
    public class ChangeXMLBase64 : IChangeXMLBase64
    {
        public string GetDeviceXMLToBase64()
        {
            string m_deviceBase64Str = LoginView.deviceXMLToBase64String;

            return m_deviceBase64Str;
        }
        public string GetLoginXMLToBase64()
        {
            string m_loginBase64Str = LoginView.loginXMLToBase64String;

            return m_loginBase64Str;
        }
    }
}