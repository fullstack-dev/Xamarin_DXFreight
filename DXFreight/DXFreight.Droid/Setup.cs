using Android.Content;
using DXFreight.Core.InitialWorking;
using DXFreight.Core.Services;
using DXFreight.Droid.InitialWorking;
using DXFreight.Droid.Services;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace DXFreight.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            Mvx.LazyConstructAndRegisterSingleton<ILocalStorage>(() => new LocalStorage());
            Mvx.LazyConstructAndRegisterSingleton<IExecuteAsRoot>(() => new ExecuteAsRoot());
            Mvx.LazyConstructAndRegisterSingleton<IGettingImei>(() => new GettingImei());
            Mvx.LazyConstructAndRegisterSingleton<IChangeXMLBase64>(() => new ChangeXMLBase64());
            Mvx.LazyConstructAndRegisterSingleton<IChangeBase64XML>(() => new ChangeBase64XML());
        }
    }
}
