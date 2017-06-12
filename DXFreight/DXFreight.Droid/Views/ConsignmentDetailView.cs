using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using DXFreight.Core.ViewModels;
using Android.Support.Design.Widget;
using Android.Graphics;
using System.Threading.Tasks;
using DXFreight.Core.Models;
using DXFreight.Core.Services;
using DXFreight.Core.APIAccess;
using MvvmCross.Platform;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for ConsignmentDetailViewModel"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    class ConsignmentDetailView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.ConsignmentDetailView;

        private ConsignmentDetailViewModel ConsignmentDetailViewModel
        {
            get
            {
                return (ConsignmentDetailViewModel)this.ViewModel;
            }
        }

        private bool logout_error;
        private static int result;
        public static string customerId = "";
        //public static string customerId = "DXTEST";

        public bool Logout_error
        {
            get { return logout_error; }
            set
            {
                logout_error = value;
                if (Logout_error)
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.consignmentdetailview);
                    Snackbar snackbar = Snackbar.Make(linearLayout, "Logout error.", Snackbar.LengthLong);
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
                else
                {    
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            string selected_menu;
            ImageButton popupmenu = FindViewById<ImageButton>(Resource.Id.popupButton);
            popupmenu.Click += (s, arg) =>
            {
                PopupMenu menu = new PopupMenu(this, popupmenu);
                menu.Inflate(Resource.Menu.popupmenu);
                menu.MenuItemClick += (si, arg1) =>
                {
                    selected_menu = arg1.Item.TitleFormatted.ToString();
                    if (selected_menu == "Show completed consignments")
                    {
                        this.ConsignmentDetailViewModel.Show_complete_consignment();
                    }
                    else if (selected_menu == "Logout")
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Logout");
                        alert.SetMessage("Are you sure?");
                        alert.SetPositiveButton("Yes", async (senderAlert, args) =>
                        {
                            await GetLogoutInfo();
                            if (result == 1)
                            {
                                LocalStorage.SaveSet("login_session_id", null);
                                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                            }
                            else
                            {
                                var linearLayout = FindViewById<LinearLayout>(Resource.Id.consignmentdetailview);
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
                    else if (selected_menu == "Use bluetooth scanner")
                    {
                        this.ConsignmentDetailViewModel.Use_bluetooth_scanner();
                    }
                    else if (selected_menu == "Use camera scanner")
                    {
                        this.ConsignmentDetailViewModel.Use_camera_scanner();
                    }
                    else if (selected_menu == "Enter SOTI Id")
                    {
                        this.ConsignmentDetailViewModel.Enter_SOTI_Id();
                    }
                    else
                    {
                        this.ConsignmentDetailViewModel.About();
                    }

                };
                menu.Show();
            };

            // Get the count value passed to us from SessionInfoView:
            //var x = Intent.Extras.GetStringArray("consignment");
            //string[] receiveconsignment = Intent.Extras.GetStringArray("newconsignment");

            ConsignmentDetailViewModel.ConsignmentinfoText = "Consignment Info";
            ConsignmentDetailViewModel.SpecialInstructionText = "Length   3m";
            ConsignmentDetailViewModel.DriverAlertText = "Driver Alert";
            ConsignmentDetailViewModel.AlertDetailsText = "5 THE COURTYARD, FURLONG ROAD, BOURNE END, BUCKINGHAMSHIRE";

            var bindingSet = this.CreateBindingSet<ConsignmentDetailView, ConsignmentDetailViewModel>();
            bindingSet.Bind().For(c => c.Logout_error).To(vm => vm.Logout_error);
            bindingSet.Apply();
        }

        public async static Task GetLogoutInfo()
        {
            var logout_result = new LogoutInfo();
            string session_id1 = Mvx.Resolve<ILocalStorage>().RetrieveSet("login_session_id");
            customerId = Mvx.Resolve<ILocalStorage>().RetrieveSet("customer");
            string url = System.String.Format("http://nxfrontend-qa.nxframework.com/NXRest.svc/logout?customer=" + customerId + "&session=" + session_id1);

            logout_result = await APIAccess.logOut(url);
            result = logout_result.LogoutResult;
        }
    }
}