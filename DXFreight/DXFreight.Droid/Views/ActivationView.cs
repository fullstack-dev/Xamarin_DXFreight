using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using DXFreight.Core.ViewModels;
using Android.Views;
using Android.Graphics;
using Android.Support.Design.Widget;
using ZXing.Mobile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for ActivationViewModel"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class ActivationView : BaseView
    { 
        protected override int LayoutResource => Resource.Layout.ActivationView;

        private static int result;
        MobileBarcodeScanner scanner;

        private ActivationViewModel ActivationViewModel
        {
            get
            {
                return (ActivationViewModel)this.ViewModel;
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
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.activation_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Invalid activation barcode - Please try again.", Snackbar.LengthLong);
                    Android.Views.View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Color.White);
                    objView.SetBackgroundColor(Color.Red);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var bindingSet = this.CreateBindingSet<ActivationView, ActivationViewModel>();
            bindingSet.Bind().For(c => c.Negative_alert).To(vm => vm.Negative_alert);
            bindingSet.Apply();
        }

        protected override void OnResume()
        {
            base.OnResume();
            custom_scan();
        }

        protected override void OnPause()
        {
            scanner.Cancel();
            base.OnPause();
            Finish();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Finish();
        }

        private async void custom_scan()
        {
            View zxingOverlay;
            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();

            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = false,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                   ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE
                }
            };
            //Tell our scanner we want to use a custom overlay instead of the default
            scanner.UseCustomOverlay = true;
            //Inflate our custom overlay from a resource layout
            zxingOverlay = LayoutInflater.FromContext(this).Inflate(Resource.Layout.ZxingOverlay, null);
            //Set our custom overlay
            scanner.CustomOverlay = zxingOverlay;
            new Task(async () =>
            {
                await Task.Delay(3000);
                scanner.AutoFocus();
            }).Start();
            //Start scanning!
            var result = await scanner.Scan(this, options);
            HandleScanResult(result);
        }

        void HandleScanResult(ZXing.Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                string barcode_result = result.Text;

                string endpoint_value = getQrcodeNode("<Endpoint>", "</Endpoint>", barcode_result);
                string code_value = getQrcodeNode("<Code>", "</Code>", barcode_result);
                string valid_value = getQrcodeNode("<Valid>", "</Valid>", barcode_result);
                string createdby_value = getQrcodeNode("<CreatedBy>", "</CreatedBy>", barcode_result);

                if (endpoint_value != null && code_value != null && valid_value != null && createdby_value != null)
                {
                    LocalStorage.SaveSet("activation_barcode", barcode_result);
                    LocalStorage.SaveSet("customer", code_value);
                    scanner.Cancel();
                    this.ActivationViewModel.Valid_activation = true;
                    this.ActivationViewModel.func();
                    this.Finish();
                }
                else
                {
                    this.ActivationViewModel.Valid_activation = false;
                    this.Finish();
                }
            }
        }

        private string getQrcodeNode(string first_tag, string last_tag, string xml)
        {
            if (xml.IndexOf(first_tag) == -1 || xml.IndexOf(last_tag) == -1)
            {
                return null;
            }
            else
            {
                int first_index = xml.IndexOf(first_tag) + first_tag.Length;
                int last_index = xml.IndexOf(last_tag) - first_index;
                string result = xml.Substring(first_index, last_index);
                return result;
            }
        }

        public override void OnBackPressed()
        {
            this.FinishAffinity();
            FinishAndRemoveTask();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            System.Environment.Exit(0);
        } 
    }
}