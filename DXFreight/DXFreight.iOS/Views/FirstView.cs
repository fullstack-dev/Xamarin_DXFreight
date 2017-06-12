using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;

namespace DXFreight.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController
    {
        public FirstView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


        }
    }
}
