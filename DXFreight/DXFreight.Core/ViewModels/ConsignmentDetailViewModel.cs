using MvvmCross.Core.ViewModels;

namespace DXFreight.Core.ViewModels
{
    public class ConsignmentDetailViewModel : MvxViewModel
    {
        string consignmentinfoText = "";
        public string ConsignmentinfoText
        {
            get { return consignmentinfoText; }
            set { SetProperty(ref consignmentinfoText, value); }
        }
        string specialInstructionText = "";
        public string SpecialInstructionText
        {
            get { return specialInstructionText; }
            set { SetProperty(ref specialInstructionText, value); }
        }
        string driverAlertText = "";
        public string DriverAlertText
        {
            get { return driverAlertText; }
            set { SetProperty(ref driverAlertText, value); }
        }
        string alertDetailsText = "";       
        public string AlertDetailsText
        {
            get { return alertDetailsText; }
            set { SetProperty(ref alertDetailsText, value); }
        }

        private bool logout_error;
        public bool Logout_error
        {
            get { return logout_error; }
            set
            {
                logout_error = value;
                RaisePropertyChanged(() => Logout_error);
            }
        }

        public ConsignmentDetailViewModel()
        {
        }

        public MvxCommand MoreCommand
        {
            get
            {
                return new MvxCommand(() => this.GotoMore());
            }
        }
        public MvxCommand PhoneCommand
        {
            get
            {
                return new MvxCommand(() => this.GotoPhone());
            }
        }
        public MvxCommand SendCommand
        {
            get
            {
                return new MvxCommand(() => this.GotoSend());
            }
        }
        public MvxCommand CheckCommand
        {
            get
            {
                return new MvxCommand(() => this.GotoCheck());
            }
        }

        public void GotoMore()
        {

        }

        public void GotoPhone()
        {

        }

        public void GotoSend()
        {

        }

        public void GotoCheck()
        {

        }

        //Top right popup menu click
        public void Show_complete_consignment()
        {
            
        }
        public void Logout()
        { 

        }
        public void Use_bluetooth_scanner()
        {

        }
        public void Use_camera_scanner()
        {

        }
        public void Enter_SOTI_Id()
        {

        }
        public void About()
        {

        }
    }
}

