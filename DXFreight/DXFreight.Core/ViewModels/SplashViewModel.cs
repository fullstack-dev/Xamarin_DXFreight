using DXFreight.Core.InitialWorking;
using MvvmCross.Core.ViewModels;
using System;
using DXFreight.Core.Services;
using MvvmCross.Platform;

namespace DXFreight.Core.ViewModels
{
    public class SplashViewModel : MvxViewModel
    {
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        private bool alert_error;
        public bool Alert_error
        {
            get { return alert_error; }
            set
            {
                alert_error = value;
                RaisePropertyChanged(() => Alert_error);
            }
        }
        private bool rooted_dialog;
        public bool Rooted_dialog
        {
            get { return rooted_dialog; }
            set
            {
                rooted_dialog = value;
                RaisePropertyChanged(() => Rooted_dialog);
            }

        }
        private bool permissionRequest;
        public bool PermissionRequest
        {
            get { return permissionRequest; }
            set
            {
                permissionRequest = value;
                RaisePropertyChanged(() => PermissionRequest);
            }
        }

        private bool permission_allow;
        public bool Permission_allow
        {
            get { return permission_allow; }
            set
            {
                permission_allow = value;
            }
        }

        public SplashViewModel()
        {
            bool rootaccess = Mvx.Resolve<IExecuteAsRoot>().CanRunRootCommands();
            if (rootaccess)
            {
                Rooted_dialog = true;
            }
            else
            {
                PermissionRequest = true;

                if (Permission_allow)
                {
                    Login();
                }                
            }          
        }

        public void Login()
        {
            //getting login date
            DateTime now = DateTime.Now.ToLocalTime();
            string currentTime = (string.Format("Current Time:{0}", now));
            var localStorage = Mvx.Resolve<ILocalStorage>();

            if (localStorage.RetrieveSet("customer") != null)
            {
               if (localStorage.RetrieveSet("login_session_id") != null)
                {
                    DateTime curDate = DateTime.Now.ToLocalTime();

                    string saved_timestamp = localStorage.RetrieveSet("login_date");
                    DateTime x = DateTime.Parse(saved_timestamp);

                  double difference_day = (curDate - x).TotalDays;

                    //I assumed that N days = 10 days
                  if (difference_day > 10)
                    {
                        this.ShowViewModel<LoginViewModel>();
                    }
                  else
                    {
                        this.ShowViewModel<ActivationViewModel>();
                    }
                }
               else
              {
                    this.ShowViewModel<LoginViewModel>();
                }
            }
            else
            {
               this.ShowViewModel<ActivationViewModel>();
            }
        }
    }
}
