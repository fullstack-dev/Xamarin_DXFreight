using Android.App;
using Android.Content.PM;
using Android.OS;
using DXFreight.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using DXFreight.Core.Services;
using System.Threading.Tasks;
using DXFreight.Core.Models;
using DXFreight.Core.APIAccess;
using Android.Views.InputMethods;
using Android.Content;
using Android.Views;
using MvvmCross.Platform;
using Android.Telephony;
using System.IO;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for LoginViewModel"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : BaseView, View.IOnTouchListener
    {
        ProgressDialog progress;
        private static int result;
        public static string customerId = "";
        //public static string customerId = "DXTEST";
        protected override int LayoutResource => Resource.Layout.LoginView;

        EditText username_edit, password_edit, depot_edit, round_edit;
        LinearLayout layout;

        public static string str_Imei = "";
        public static string deviceXMLToBase64String = "";
        public static string loginXMLToBase64String = "";

        string username = "";
        public string Username
        {
            get { return username; }
            set {  username = value; }
        }
        // login loading indicator
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {   
                isLoading = value;

                if(isLoading)
                {
                    progress = new ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                    progress.SetMessage("Please wait...");
                    progress.SetCancelable(false);
                    progress.Show();
                }
                else
                {
                    progress.Hide();
                    progress.Dismiss();
                }
            }
        }
        // login success
        private bool login_success;
        public bool Login_success
        {
            get { return login_success; }
            set
            {
                login_success = value;

                if (login_success)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Log in successful", Snackbar.LengthLong);
                    View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Color.White);
                    objView.SetBackgroundColor(Color.Green);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();
                }
            }
        }
        // login error alert
        private bool other_error;
        public bool Other_error
        {
            get { return other_error; }
            set
            {
                other_error = value;

                if (other_error)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Unable to authenticate", Snackbar.LengthLong);
                    View objView = snackbar.View;
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
        private bool baduser_error;
        public bool Baduser_error
        {
            get { return baduser_error; }
            set
            {
                baduser_error = value;
                if (baduser_error)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Please provide correct credentials", Snackbar.LengthLong);
                    View objView = snackbar.View;
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
        private bool session_exist;
        public bool Session_exist
        {
            get { return session_exist; }
            set
            {
                session_exist = value;
                if (session_exist)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "User already logged in elsewhere", Snackbar.LengthLong);
                    Android.Views.View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Color.White);
                    objView.SetBackgroundColor(Color.Green);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();
                }
            }
        }
        //Depot Status
        private bool depot_status;
        public bool Depot_status
        {
            get { return depot_status; }
            set
            {
                depot_status = value;
                if (depot_status)
                {
                    FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab1);
                    Android.Content.Res.ColorStateList csl = new Android.Content.Res.ColorStateList(new int[][] { new int[0] }, new int[] { Color.ParseColor("#009e11") });
                    fab.BackgroundTintList = csl;
                }
            }
        }
        private bool starting_round;
        public bool Starting_round
        {
            get { return starting_round; }
            set
            {
                starting_round = value;
                if (starting_round)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Starting round", Snackbar.LengthLong);
                    Android.Views.View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Color.White);
                    objView.SetBackgroundColor(Color.Green);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();
                }
            }
        }
        private bool invalid_round;
        public bool Invalid_round
        {
            get { return invalid_round; }
            set
            {
                invalid_round = value;
                if (invalid_round)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Enter valid round details", Snackbar.LengthLong);
                    View objView = snackbar.View;
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
        private bool timeout;
        public bool Timeout
        {
            get { return timeout; }
            set
            {
                timeout = value;
                if (timeout)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Round was not started before timeout.", Snackbar.LengthLong);
                    View objView = snackbar.View;
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
        // connected notification
        private bool connection;
        public bool Connection
        {
            get { return connection; }
            set
            {
                connection = value;

                if (!connection)
                {
                }
            }
        }

        public string getImeiValue()
        {
            Android.Telephony.TelephonyManager mTelephonyMgr;  
            mTelephonyMgr = (TelephonyManager)GetSystemService(TelephonyService);
            //IMEI number  
            string m_deviceId = mTelephonyMgr.DeviceId;
            return m_deviceId;
        }

        public string getDeviceXMLToBase64()
        {
            // file route
            var x = Resources.GetString(Resource.Raw.device);

            var content = Resources.OpenRawResource(Resource.Raw.device);
            var stringContent = string.Empty;

            using (StreamReader sr = new StreamReader(content))
            {
                stringContent = sr.ReadToEnd();
            }
            byte[] toBytes = System.Text.Encoding.ASCII.GetBytes(stringContent);
            string base64 = Android.Util.Base64.EncodeToString(toBytes, Android.Util.Base64Flags.Default);

            return base64;
        }

        public string getLoginXMLToBase64()
        {
            // file route
            var x = Resources.GetString(Resource.Raw.login);

            var content = Resources.OpenRawResource(Resource.Raw.device);
            var stringContent = string.Empty;

            using (StreamReader sr = new StreamReader(content))
            {
                stringContent = sr.ReadToEnd();
            }
            byte[] toBytes = System.Text.Encoding.ASCII.GetBytes(stringContent);
            string base64 = Android.Util.Base64.EncodeToString(toBytes, Android.Util.Base64Flags.Default);

            return base64;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            str_Imei = getImeiValue();

            deviceXMLToBase64String = getDeviceXMLToBase64();
            loginXMLToBase64String = getLoginXMLToBase64();

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab1);
            Android.Content.Res.ColorStateList csl = new Android.Content.Res.ColorStateList(new int[][] { new int[0] }, new int[] { Android.Graphics.Color.ParseColor("#999999") });
            fab.BackgroundTintList = csl;

            layout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
            layout.SetOnTouchListener(this);
            username_edit = FindViewById<EditText>(Resource.Id.username);
            password_edit = FindViewById<EditText>(Resource.Id.password);
            depot_edit = FindViewById<EditText>(Resource.Id.depot);
            round_edit = FindViewById<EditText>(Resource.Id.round);

            var bindingSet = this.CreateBindingSet<LoginView, LoginViewModel>();

            bindingSet.Bind().For(c => c.Username).To(vm => vm.Username);
            bindingSet.Bind().For(c => c.IsLoading).To(vm => vm.IsLoading);
            bindingSet.Bind().For(c => c.Baduser_error).To(vm => vm.Baduser_error);
            bindingSet.Bind().For(c => c.Session_exist).To(vm => vm.Session_exist);
            bindingSet.Bind().For(c => c.Other_error).To(vm => vm.Other_error);
            bindingSet.Bind().For(c => c.Login_success).To(vm => vm.Login_success);
            bindingSet.Bind().For(c => c.Depot_status).To(vm => vm.Depot_status);
            bindingSet.Bind().For(c => c.Starting_round).To(vm => vm.Starting_round);
            bindingSet.Bind().For(c => c.Invalid_round).To(vm => vm.Invalid_round);
            bindingSet.Bind().For(c => c.Timeout).To(vm => vm.Timeout);
            bindingSet.Bind().For(c => c.Connection).To(vm => vm.Connection);

            bindingSet.Apply();
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Please confirm whether to close the application or not.");
            alert.SetPositiveButton("Yes", async (senderAlert, args) =>
            {
                await GetLogoutInfo();
                if (result == 1)
                {
                    LocalStorage.SaveSet("login_session_id", null);

                    this.FinishAffinity();
                    FinishAndRemoveTask();
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    //System.Environment.Exit(0);
                }
                else
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.login_content_frame);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Log out error!", Snackbar.LengthLong);
                    View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Color.White);
                    objView.SetBackgroundColor(Color.Red);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();

                    this.FinishAffinity();
                    FinishAndRemoveTask();
                    //System.Environment.Exit(0);
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                }
            });
            alert.SetNegativeButton("No", (senderAlert, args) =>
            {

            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }

        public async static Task GetLogoutInfo()
        {
            var logout_result = new LogoutInfo();

            customerId = Mvx.Resolve<ILocalStorage>().RetrieveSet("customer");
            string url = Java.Lang.String.Format("http://nxfrontend-qa.nxframework.com/NXRest.svc/logout?customer=" + customerId + "&session=" + Mvx.Resolve<ILocalStorage>().RetrieveSet("login_session_id"));

            logout_result = await APIAccess.logOut(url);
            result = logout_result.LogoutResult;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(username_edit.WindowToken, 0);
                    break;
            }
            return true;
        }
    }
}
