using DXFreight.Core.Models;
using DXFreight.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using System.Net.Http;

namespace DXFreight.Core.ViewModels
{
    public class LoginViewModel
        : MvxViewModel
    {
        public static string login_session_id = "";
        public static string login_username = "";
        DateTime curLoginDate;
        public string customerId = null;
        //public string customerId = "DXTEST";

        public LoginViewModel()
        {
        }

        string username = "";
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        string password = "";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        string depot = "";
        public string Depot
        {
            get { return depot; }
            set { SetProperty(ref depot, value); }
        }
        string round = "";
        public string Round
        {
            get { return round; }
            set { SetProperty(ref round, value); }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged(()=>IsLoading);
            }
        }
        private bool login_success;
        public bool Login_success
        {
            get { return login_success; }
            set
            {
                login_success = value;
                RaisePropertyChanged(() => Login_success);
            }
        }
        private bool baduser_error;
        public bool Baduser_error
        {
            get { return baduser_error; }
            set
            {
                baduser_error = value;
                RaisePropertyChanged(() => Baduser_error);
            }
        }
        private bool session_exist;
        public bool Session_exist
        {
            get { return session_exist; }
            set
            {
                session_exist = value;
                RaisePropertyChanged(() => Session_exist);
            }
        }
        private bool other_error;
        public bool Other_error
        {
            get { return other_error; }
            set
            {
                other_error = value;
                RaisePropertyChanged(() => Other_error);
            }
        }
        private bool depot_status;
        public bool Depot_status
        {
            get { return depot_status; }
            set
            {
                depot_status = value;
                RaisePropertyChanged(() => Depot_status);
            }
        }
        private bool starting_round;
        public bool Starting_round
        {
            get { return starting_round; }
            set
            {
                starting_round = value;
                RaisePropertyChanged(() => Starting_round);
            }
        }
        private bool invalid_round;
        public bool Invalid_round
        {
            get { return invalid_round; }
            set
            {
                invalid_round = value;
                RaisePropertyChanged(() => Invalid_round);
            }
        }
        private bool timeout;
        public bool Timeout
        {
            get { return timeout; }
            set
            {
                timeout = value;
                RaisePropertyChanged(() => Timeout);
            }
        }
        private bool connection;
        public bool Connection
        {
            get { return connection; }
            set
            {
                connection = value;
                RaisePropertyChanged(() => Connection);
            }
        }
        private bool sending_info;
        public bool Sending_info
        {
            get { return sending_info; }
            set
            {
                sending_info = value;
                RaisePropertyChanged(() => Sending_info);
            }
        }

        public MvxCommand GotoSessionInfoCommand
        {
            get
            {
                return new MvxCommand(async () => await this.GotoSessionInfo());
            }
        }

        public MvxCommand GotoDepotCommand
        {
            get
            {
                return new MvxCommand(async () => await this.GotoDepot());
            }
        }

        private async Task GotoSessionInfo()
        {
            Depot_status = false;

            if(Username.Equals("") || Password.Equals(""))
            {
                Baduser_error = true;
            }else
            {
                Baduser_error = false;

                var login_userinfo = new UserInfo();
                int login_result;

                string uuid = Mvx.Resolve<IGettingImei>().DeviceImei();
                customerId = Mvx.Resolve<ILocalStorage>().RetrieveSet("customer");             
                
                string url = $"http://nxfrontend-qa.nxframework.com/NXRest.svc/login?customer={customerId}&device={username}&password={password}&uuid={uuid}";

                IsLoading = true;

                var localStorage = Mvx.Resolve<ILocalStorage>();

                login_userinfo = await APIAccess.APIAccess.getSessionId(url);
                login_result = login_userinfo.LoginResult.Result;
                if (login_result == 1)
                {
                    IsLoading = false;
                    Login_success = true;
                    Depot_status = true;
                    //getting session id
                    login_session_id = login_userinfo.LoginResult.Session;

                    localStorage.SaveSet("login_session_id", login_session_id);
                    string temp = localStorage.RetrieveSet("login_session_id");
                    string login_date = login_userinfo.LoginResult.ServerTime;
                    string login_date_timestamp = login_date.Substring(6, 13);

                    localStorage.SaveSet("login_date", login_date_timestamp);
                    //username
                    login_username = Username;
                    //login time 
                    curLoginDate = DateTime.Now.ToLocalTime();

                    //this.ShowViewModel<SessionInfoViewModel>();
                }else if (login_result == -5)
                {
                    IsLoading = false;
                    Session_exist = true;
                    Depot_status = false;

                    login_session_id = login_userinfo.LoginResult.Session;

                    localStorage.SaveSet("login_session_id", login_session_id);
                }
                else
                {
                    IsLoading = false;
                    Other_error = true;
                    Depot_status = false;
                }
            }
        }

        private async Task GotoDepot()
        {
            if (Depot_status)
            {
                if (Depot.Equals("") || Round.Equals(""))
                {
                    Invalid_round = true;
                }
                else
                {
                    //I assumed current entered value is 999, 99.
                    if (Depot.Equals("999") && Round.Equals("99"))
                    {
                        Invalid_round = false;
                        //Detect Timeout
                        DateTime curDepotDate = DateTime.Now.ToLocalTime();
                        string curDepotDateStr = curLoginDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DateTime x = curLoginDate;
                        double difference_minute = (curDepotDate - x).TotalMinutes;                        

                        if(difference_minute >= 5)
                        {
                            Timeout = true;
                        }else
                        {
                            Starting_round = true;
                            customerId = Mvx.Resolve<ILocalStorage>().RetrieveSet("customer");

                            //post the device information message to the NX Framework, clear any consignment already stored in the local database
                            //
                            var deviceUrl = "http://nxfrontend-qa.nxframework.com/NXRest.svc/senddata?customer=" + customerId + "&session=" + login_session_id + "&metatype=17&filename=device.xml&notes=Sent&datetime="+curDepotDateStr+"&priority=3";

                            string deviceInfo = Mvx.Resolve<IChangeXMLBase64>().GetDeviceXMLToBase64();
                            var deviceXMLContent = new StringContent(deviceInfo);

                            var senddataresult = new DataInfo();

                            senddataresult = await APIAccess.APIAccess.SendData(deviceUrl, deviceXMLContent);
                            if(senddataresult.SendDataResult.Result == 1)
                            {
                                var loginUrl = "http://nxfrontend-qa.nxframework.com/NXRest.svc/senddata?customer=" + customerId + "&session=" + login_session_id + "&metatype=17&filename=login.xml&notes=Sent&datetime="+curDepotDateStr+"&priority=3";

                                string loginInfo = Mvx.Resolve<IChangeXMLBase64>().GetLoginXMLToBase64();
                                var loginXMLContent = new StringContent(loginInfo);

                                var sendloginresult = new DataInfo();

                                sendloginresult = await APIAccess.APIAccess.SendData(loginUrl, loginXMLContent);
                                if(sendloginresult.SendDataResult.Result == 1)
                                {
                                    this.ShowViewModel<ConsignmentSummaryViewModel>();
                                }else
                                {
                                    // Sending Login.XML failure
                                }
                            }
                            else
                            {
                                // Sending Device.XML failure
                            }
                        }
                    }
                    else
                        Invalid_round = true;
                }
            }
        }

    }
    public class Item
    {
        public string customer { get; set; }
        public string device { get; set; }
        public string password { get; set; }    
    }
}
