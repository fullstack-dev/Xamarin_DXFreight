using Android.OS;
using Android.Support.V7.Widget;
using DXFreight.Core.Services;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;

namespace DXFreight.Droid.Views
{
    public abstract class BaseView : MvxAppCompatActivity
    {
        protected ILocalStorage LocalStorage;

        protected Toolbar Toolbar { get; set; }        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            LocalStorage = Mvx.Resolve<ILocalStorage>();

            SetContentView(LayoutResource);

            //Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //if (Toolbar != null)
            //{
            //    SetSupportActionBar(Toolbar);
            //    SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //    SupportActionBar.SetHomeButtonEnabled(true);
            //}
        }
        
        protected abstract int LayoutResource { get; }
    }
}
