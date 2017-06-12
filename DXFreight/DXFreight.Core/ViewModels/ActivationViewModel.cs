using MvvmCross.Core.ViewModels;

namespace DXFreight.Core.ViewModels
{
    public class ActivationViewModel : MvxViewModel
    {
        private bool negative_alert;
        public bool Negative_alert
        {
            get { return negative_alert; }
            set
            {
                negative_alert = value;
                RaisePropertyChanged(() => Negative_alert);
            }
        }

        private bool valid_activation;
        public bool Valid_activation
        {
            get { return valid_activation; }
            set
            {
                valid_activation = value;
            }
        }

        public ActivationViewModel()
        {
            func();           
        }

        public void func()
        {
            //if the barcode is a valid activation barcode
            if (Valid_activation)
            {
                this.ShowViewModel<LoginViewModel>();
            }
            else
            {
                Negative_alert = true;
            }
        }
    }
}
