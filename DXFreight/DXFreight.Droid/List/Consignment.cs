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

namespace DXFreight.Droid.List
{
    class Consignment
    {
        public bool CheckBox { get; set; }
        public string CT { get; set; }
        public string ConNum { get; set; }
        public string Items { get; set; }
        public string Times { get; set; }
        public string TS { get; set; }
        public string Address { get; set; }
    }
}