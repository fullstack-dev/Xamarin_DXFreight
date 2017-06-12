using DXFreight.Core.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Threading.Tasks;

namespace DXFreight.Core.ViewModels
{
    public class SessionInfoViewModel : MvxViewModel
    {
        public static int result;
        public static string session_id1;

        private string session_id = LoginViewModel.login_session_id;
        public string Session_id
        {
            get { return session_id; }
            set { session_id = value; }
        }
        private string login_username = LoginViewModel.login_username;
        public string Login_username
        {
            get { return login_username; }
            set { login_username = value; }
        }
        private bool new_consigment;
        public bool New_consigment
        {
            get { return new_consigment; }
            set
            {
                new_consigment = value;
                RaisePropertyChanged(() => New_consigment);
            }
        }
        private bool update_consigment;
        public bool Update_consigment
        {
            get { return update_consigment; }
            set
            {
                update_consigment = value;
                RaisePropertyChanged(() => Update_consigment);
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

        public SessionInfoViewModel()
        {
            session_id1 = session_id;

            //if new consignment
            New_consigment = true;
            Update_consigment = true;
            //if connected, connected notification.
            Connection = true;

        }

        public async static Task GetLogoutInfo()
        { 
            var logout_result = new LogoutInfo();

            string customerid = "DASYS";
            string url = String.Format("http://nxfrontend-qa.nxframework.com/NXRest.svc/logout?customer=" + customerid + "&session=" + session_id1);

            logout_result = await APIAccess.APIAccess.logOut(url);
            result = logout_result.LogoutResult;
        }

        
    }
}
