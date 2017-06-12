using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using DXFreight.Core.ViewModels;
using Android.Content;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Graphics;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for SOTIViewModel"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SOTIView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.SOTIView;

        private bool positive_alert;
        public bool Positive_alert
        {
            get { return positive_alert; }
            set
            {
                positive_alert = value;
                if (positive_alert)
                {
                    //View view = LayoutInflater.Inflate(Resource.Layout.PositiveToast, null);
                    //var txt = view.FindViewById<TextView>(Resource.Id.txtCustomToast);
                    //txt.Text = "Activation was successful.";
                    //var toast = new Toast(this)
                    //{
                    //    Duration = ToastLength.Long,
                    //    View = view
                    //};
                    //toast.Show();
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.SOTI_view);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Activation was successful.", Snackbar.LengthLong);
                    Android.Views.View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Android.Graphics.Color.White);
                    objView.SetBackgroundColor(Color.Green);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Android.Graphics.Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();
                    //var metrics = Resources.DisplayMetrics;
                    //var menuWidth = metrics.WidthPixels;
                    //RelativeLayout.LayoutParams parameters = new RelativeLayout.LayoutParams(menuWidth, 300);
                    //parameters.AddRule(LayoutRules.AlignParentBottom);
                    //objView.LayoutParameters = parameters;
                }
            }
        }

        private bool negative_alert;
        public bool Negative_alert
        {
            get { return negative_alert; }
            set
            {
                negative_alert = value;
                if (negative_alert)
                {
                    //Toast.MakeText(this, "Invalid activation barcode - please try again.", ToastLength.Long).Show();
                    View view = LayoutInflater.Inflate(Resource.Layout.NegativeToast, null);
                    var txt = view.FindViewById<TextView>(Resource.Id.txtCustomToast);
                    txt.Text = "Invalid activation barcode - please try again.";
                    var toast = new Toast(this)
                    {
                        Duration = ToastLength.Long,
                        View = view
                    };
                    toast.Show();
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var bindingSet = this.CreateBindingSet<SOTIView, SOTIViewModel>();
            bindingSet.Bind().For(c => c.Positive_alert).To(vm => vm.Positive_alert);
            bindingSet.Bind().For(c => c.Negative_alert).To(vm => vm.Negative_alert);
            bindingSet.Apply();
        }
    }
}