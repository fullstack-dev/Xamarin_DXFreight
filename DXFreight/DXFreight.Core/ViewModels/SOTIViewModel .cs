using MvvmCross.Core.ViewModels;

namespace DXFreight.Core.ViewModels
{
    public class SOTIViewModel : MvxViewModel
    {
        string sotiId = "";
        public string SotiId
        {
            get { return sotiId; }
            set { SetProperty(ref sotiId, value); }
        }
        public MvxCommand NextCommand
        {
            get
            {
                return new MvxCommand( () =>  this.GotoNext());
            }
        }
        public MvxCommand CheckCommand
        {
            get
            {
                return new MvxCommand( () => this.GotoCheck());
            }
        }

        private bool positive_alert;
        public bool Positive_alert
        {
            get { return positive_alert; }
            set
            {
                positive_alert = value;
                if (positive_alert)
                {

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

                }
            }
        }

        public SOTIViewModel()
        {

        }

        public void GotoNext()
        {

        }

        public void GotoCheck()
        {

        }
    }
}
