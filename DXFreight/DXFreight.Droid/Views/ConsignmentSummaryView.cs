using Android.App;
using Android.OS;
using Android.Content.PM;
using MvvmCross.Binding.BindingContext;
using DXFreight.Core.ViewModels;
using Android.Widget;
using Android.Support.Design.Widget;
using DXFreight.Core.Services;
using Android.Graphics;
using DXFreight.Core.Models;
using DXFreight.Core.APIAccess;
using System.Threading.Tasks;
using Android.Views;
using Android.Views.InputMethods;
using Android.Content;
using System.Collections.Generic;
using DXFreight.Droid.List;
using System.Linq;
using MvvmCross.Platform;
using Android.Support.V4.App;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for LoginViewModel"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class ConsignmentSummaryView : BaseView, View.IOnTouchListener
    {
        private static int result;
        public static string customerId = null;
        //public static string customerId = "DXTEST";

        AlertDialog.Builder builder;
        AlertDialog alert;

        EditText search_edit;
        LinearLayout layout;

        private List<Consignment> mConsignment;
        ListView mListView;
        private ConsignmentAdapter mAdapter;

        bool[] checkBox = new bool[100];
        string[] cT = new string[100];
        string[] conNum = new string[100];
        string[] items = new string[100];
        string[] times = new string[100];
        string[] tS = new string[100];
        string[] address = new string[100];

        int count = 0;// Initialize the number of consignment items.

        public static string selected_cT = null;
        public static string selected_conNum = null;
        public static string selected_items = null;
        public static string selected_times = null;
        public static string selected_tS = null;
        public static string selected_address = null;

        private ConsignmentSummaryViewModel ConsignmentSummaryViewModel
        {
            get
            {
                return (ConsignmentSummaryViewModel)this.ViewModel;
            }
        }

        protected override int LayoutResource => Resource.Layout.ConsignmentSummaryView;

        private bool waiting_data;
        public bool Waiting_data
        {
            get { return waiting_data; }
            set
            {
                waiting_data = value;

                if (waiting_data)
                {
                    builder = new AlertDialog.Builder(this);
                    builder.SetView(Resource.Layout.Wait);
                    alert = builder.Create();
                    alert.Show();
                }
                else
                {
                    alert.Dismiss();
                    string dataXMLStr = ConsignmentSummaryViewModel.dataXMLStr;
                    HandleConsignmentXML(dataXMLStr);
                }
            }
        }

        void HandleConsignmentXML(string result)
        {
            string partXMLString = "";
            if (result != null && !string.IsNullOrEmpty(result))
            {
                string wholeXMLString = result;                
                mConsignment = new List<Consignment>();
                while (wholeXMLString.IndexOf("<Consignment>") != -1)
                {
                    partXMLString = getConsignmentNode("<Consignment>", "</Consignment>", wholeXMLString);
                    string restXMLString = wholeXMLString.Substring(wholeXMLString.IndexOf("</Consignment>") + 14);
                    wholeXMLString = restXMLString;

                    checkBox[count] = false;
                    cT[count] = getConsignmentNode("<AddressType>", "</AddressType>", partXMLString);
                    conNum[count] = getConsignmentNode("<Number>", "</Number>", partXMLString);
                    items[count] = getConsignmentNode("<NoItems>", "</NoItems>", partXMLString);
                    times[count] = getConsignmentNode("<eventPlanTime>", "</eventPlanTime>", partXMLString);
                    tS[count] = getConsignmentNode("<TimeSlot>", "</TimeSlot>", partXMLString);
                    address[count] = getConsignmentNode("<AddressLine1>", "</AddressLine1>", partXMLString) + "," + getConsignmentNode("<AddressLine2>", "</AddressLine2>", partXMLString) + "," + getConsignmentNode("<AddressLine3>", "</AddressLine3>", partXMLString) + "," + getConsignmentNode("<AddressLine4>", "</AddressLine4>", partXMLString + "," + getConsignmentNode("<AddressPostcode>", "</AddressPostcode>", partXMLString));

                    mConsignment.Add(new Consignment { CheckBox = checkBox[count], CT = cT[count], ConNum = conNum[count], Items = items[count], Times = times[count], TS = tS[count], Address = address[count] });
                    count += 1;

                    if (wholeXMLString.IndexOf("<Consignment>") == -1)
                        break;
                }

                mAdapter = new ConsignmentAdapter(this, Resource.Layout.row_consignment, mConsignment);
                mListView.Adapter = mAdapter;

                mListView.ItemClick += mListView_ItemClick;

                TextView tv1 = (TextView)FindViewById(Resource.Id.title);
                tv1.Text = "Total: " + count.ToString() +", Remaining: "+ count.ToString() +", Completed: " + "0" ;
            }
        }

        private string getConsignmentNode(string first_tag, string last_tag, string xml)
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Avoid, Layout Up 
            Window.SetSoftInputMode(SoftInput.AdjustPan);

            layout = FindViewById<LinearLayout>(Resource.Id.consignmentsummaryview);
            layout.SetOnTouchListener(this);
            search_edit = FindViewById<EditText>(Resource.Id.inputSearch);
            mListView = FindViewById<ListView>(Resource.Id.listView);

            search_edit.TextChanged += mSearch_TextChanged;

            // Popup Menu
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

                                this.FinishAffinity();
                                FinishAndRemoveTask();
                                Process.KillProcess(Process.MyPid());
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                var linearLayout = FindViewById<LinearLayout>(Resource.Id.consignmentdetailview);
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
                                Process.KillProcess(Process.MyPid());
                                System.Environment.Exit(0);
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
                    }
                    else if (selected_menu == "Use camera scanner")
                    {
                    }
                    else if (selected_menu == "Enter SOTI Id")
                    {
                    }
                    else
                    {
                    }
                };
                menu.Show();
            };

            var bindingSet = this.CreateBindingSet<ConsignmentSummaryView, ConsignmentSummaryViewModel>();
            bindingSet.Bind().For(c => c.Waiting_data).To(vm => vm.Waiting_data);
            bindingSet.Apply();
        }

        void mSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<Consignment> searchedConsignment = (from consignment in mConsignment
                                            where consignment.CT.Contains(search_edit.Text) || consignment.ConNum.Contains(search_edit.Text)
                                            || consignment.Items.Contains(search_edit.Text) || consignment.Times.Contains(search_edit.Text)
                                            || consignment.TS.Contains(search_edit.Text) || consignment.Address.Contains(search_edit.Text)
                                                     select consignment).ToList<Consignment>();

            mAdapter = new ConsignmentAdapter(this, Resource.Layout.row_consignment, searchedConsignment);
            mListView.Adapter = mAdapter;
        }

        // List item click event
        void mListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selected_cT = mConsignment[e.Position].CT;
            selected_conNum = mConsignment[e.Position].ConNum;
            selected_items = mConsignment[e.Position].Items;
            selected_times = mConsignment[e.Position].Times;
            selected_tS = mConsignment[e.Position].TS;
            selected_address = mConsignment[e.Position].Address;

            //Toast.MakeText(this, mConsignment[e.Position].Address, ToastLength.Long).Show();
            ConsignmentSummaryViewModel.SelectItem();
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
                    Process.KillProcess(Process.MyPid());
                    System.Environment.Exit(0);
                }
                else
                {
                    var linearLayout = FindViewById<LinearLayout>(Resource.Id.consignmentsummaryview);
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
                    Process.KillProcess(Process.MyPid());
                    System.Environment.Exit(0);
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
            string sessionId = Mvx.Resolve<ILocalStorage>().RetrieveSet("login_session_id");
            customerId = Mvx.Resolve<ILocalStorage>().RetrieveSet("customer");
            string url = System.String.Format("http://nxfrontend-qa.nxframework.com/NXRest.svc/logout?customer=" + customerId + "&session=" + sessionId);
            logout_result = await APIAccess.logOut(url);
            result = logout_result.LogoutResult;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(search_edit.WindowToken, 0);
                    break;
            }
            return true;
        }
    }
}