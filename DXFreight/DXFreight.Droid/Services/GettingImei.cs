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
using Android.Telephony;

namespace DXFreight.Droid.Services
{
    public class GettingImei : IGettingImei
    {
        public string DeviceImei()
        {
            string m_deviceId = LoginView.str_Imei;

            return m_deviceId;
        }
    }
}