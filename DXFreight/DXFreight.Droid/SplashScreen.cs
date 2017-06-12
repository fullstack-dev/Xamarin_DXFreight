using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Views;
using System.Threading.Tasks;
using Plugin.Permissions;
using Android.Widget;

namespace DXFreight.Droid
{
    [Activity(
        Label = "DX ISP"
        , MainLauncher = true
        , Icon = "@mipmap/ic_launcher"
        , Theme = "@style/Theme.Splash"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
            
        }
    }
}
