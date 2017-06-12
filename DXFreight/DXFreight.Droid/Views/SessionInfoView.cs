using DXFreight.Core.ViewModels;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;
using MvvmCross.Binding.BindingContext;
using DXFreight.Core.Services;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Graphics;
using MvvmCross.Platform;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for SessionInfoViewModel"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SessionInfoView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.SessionInfoView;

        private static readonly int NewConsignmentNotification = 9997;
        private static readonly int UpdateConsignmentNotification = 9998;
        private static readonly int ConnectionStatusNotification = 9999;

        private string login_username = LoginViewModel.login_username;
        public string Login_username
        {
            get { return login_username; }
            set { login_username = value; }
        }
        
        //new consignment notificaiton
        private bool new_consigment;
        public bool New_consigment
        {
            get { return new_consigment; }
            set
            {
                new_consigment = value;
                if (new_consigment)
                {
                    //if example following consignment
                    string consignment_number = "L1111111";
                    string consignment_addressline1 = "5 THE COURTYARD";
                    string consignment_addressline2 = "FURLONG ROAD";
                    string consignment_addressline3 = "BOURNE END";
                    string consignment_addressline4 = "BUCKINGHAMSHIRE";
                    string consignment_addresspostcode = "SL8 5AU";
                    string consignment_info = "consignment info";
                    string consignment_specialinstruction = "Length   3m";
                    string consignment_driveralert = "Driver Alert";

                    string[] newconsignment = { consignment_info, consignment_specialinstruction, consignment_driveralert, consignment_addressline1, consignment_addressline2, consignment_addressline3, consignment_addressline4, consignment_addresspostcode};

                    Bundle valuesForActivity = new Bundle();
                    //valuesForActivity.PutStringArray("consignment", newconsignment);
                    valuesForActivity.PutInt("count", 1);

                    // When the user clicks the notification, ConsingmentDetailView will start up.
                    Intent newConsignmentIntent = new Intent(this, typeof(ConsignmentDetailView));
                    // Pass some values to ConsingmentDetailView:
                    newConsignmentIntent.PutExtras(valuesForActivity);
                    // Construct a back stack for cross-task navigation:
                    Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(ConsignmentDetailView)));
                    stackBuilder.AddNextIntent(newConsignmentIntent);

                    // Create the PendingIntent with the back stack:            
                    PendingIntent newConsignmentPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

                    string consignmentno_string = Java.Lang.String.Format("Cons No: %s", consignment_number);
                    string consignmentaddress_string = Java.Lang.String.Format("Address: %s, %s, %s, %s, $s", consignment_addressline1, consignment_addressline2, consignment_addressline3, consignment_addressline4, consignment_addresspostcode);

                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetAutoCancel(true)
                        .SetContentIntent(newConsignmentPendingIntent)
                        .SetContentTitle("New Consignment")
                        .SetSmallIcon(Resource.Mipmap.ic_add_circle_outline_white_24dp)
                        .SetContentText(consignmentno_string)
                        .SetSubText(consignmentaddress_string);

                    NotificationManager notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
                    notificationManager.Notify(NewConsignmentNotification, builder.Build());
                }
            }
        }
        private bool update_consigment;
        public bool Update_consigment
        {
            get { return update_consigment; }
            set
            {
                update_consigment = value;
                if (update_consigment)
                {
                    // When the user clicks the notification, SessionInfoView will start up.
                    Intent updateConsignmentIntent = new Intent(this, typeof(ConsignmentDetailView));
                    // Construct a back stack for cross-task navigation:
                    Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(ConsignmentDetailView)));
                    stackBuilder.AddNextIntent(updateConsignmentIntent);

                    // Create the PendingIntent with the back stack:            
                    PendingIntent updateConsignmentPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

                    string consignment_number = "L1111111";

                    string consignmentno_string = Java.Lang.String.Format("Cons No: %s", consignment_number);

                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetAutoCancel(true)
                        .SetContentIntent(updateConsignmentPendingIntent)
                        .SetContentTitle("Consignment Update")
                        .SetSmallIcon(Resource.Mipmap.ic_update_white_24dp)
                        .SetContentText(consignmentno_string);

                    NotificationManager notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
                    notificationManager.Notify(UpdateConsignmentNotification, builder.Build());
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

                if (connection)
                {
                    // When the user clicks the notification, SessionInfoView will start up.
                    Intent resultIntent = new Intent(this, typeof(SessionInfoView));
                    // Construct a back stack for cross-task navigation:
                    Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(SessionInfoView)));
                    stackBuilder.AddNextIntent(resultIntent);

                    // Create the PendingIntent with the back stack:            
                    PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

                    string conect_string = Java.Lang.String.Format("Status: Connected");
                    string driver_string = Java.Lang.String.Format("Driver: %s", Login_username);
                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetAutoCancel(false)
                        .SetContentIntent(resultPendingIntent)
                        .SetContentTitle("DX ISP")
                        .SetSmallIcon(Resource.Mipmap.ic_info_outline_white_24dp)
                        .SetContentText(conect_string)
                        .SetSubText(driver_string);

                    builder.SetPriority((int)NotificationPriority.Max);
                    builder.SetOngoing(true);

                    NotificationManager notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
                    notificationManager.Notify(ConnectionStatusNotification, builder.Build());
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var bindingSet = this.CreateBindingSet<SessionInfoView, SessionInfoViewModel>();

            bindingSet.Bind().For(c => c.Login_username).To(vm => vm.Login_username);
            bindingSet.Bind().For(c => c.New_consigment).To(vm => vm.New_consigment);
            bindingSet.Bind().For(c => c.Update_consigment).To(vm => vm.Update_consigment);
            bindingSet.Bind().For(c => c.Connection).To(vm => vm.Connection);

            bindingSet.Apply();
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Please confirm whether to close the application or not.");
            alert.SetPositiveButton("Yes", async (senderAlert, args) =>
            {
                await SessionInfoViewModel.GetLogoutInfo();
                if(SessionInfoViewModel.result == 1)
                {
                    LocalStorage.SaveSet("login_session_id", null);
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                }
                else
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.session_info_view);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Log out error!", Snackbar.LengthLong);
                    Android.Views.View objView = snackbar.View;
                    TextView txtAction = objView.FindViewById<TextView>(Resource.Id.snackbar_action);
                    txtAction.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    txtAction.SetTextColor(Android.Graphics.Color.White);
                    objView.SetBackgroundColor(Color.Red);
                    //set message text color
                    TextView txtMessage = objView.FindViewById<TextView>(Resource.Id.snackbar_text);
                    txtMessage.SetTextColor(Android.Graphics.Color.White);
                    txtMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
                    snackbar.Show();
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
    }

}
