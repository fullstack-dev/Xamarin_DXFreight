using DXFreight.Core.Models;
using DXFreight.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DXFreight.Core.ViewModels
{
    public class ConsignmentSummaryViewModel: MvxViewModel
    {
        public string customer = null;
        //public string customer = "DXTEST";
        public string login_Session_Id = null;
        public string guid = null;
        public string dataXMLStr = "";
        public bool correctFlag = false;

        public ConsignmentSummaryViewModel()
        {
            uploadingSummary();            
        }

        private bool waiting_data;
        public bool Waiting_data
        {
            get { return waiting_data; }
            set
            {
                waiting_data = value;
                RaisePropertyChanged(() => Waiting_data);
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                RaisePropertyChanged(() => SearchString);
            }
        }

        public void SelectItem()
        {
            this.ShowViewModel<ConsignmentDetailViewModel>();
        }

        public async void uploadingSummary()
        {
            Waiting_data = true;
            await pingRequest();            
        }

        public async Task pingRequest()
        {
            var localStorage = Mvx.Resolve<ILocalStorage>();
            login_Session_Id = localStorage.RetrieveSet("login_session_id");
            customer = localStorage.RetrieveSet("customer");
            string base_url = "http://nxfrontend-qa.nxframework.com/NXRest.svc/ping?customer="+customer+"&session="+ login_Session_Id;
            var pingresult = new PingInfo();
            pingresult = await APIAccess.APIAccess.Ping(base_url);
            if (pingresult.PingResult.DataPending > 0)
            {
                //guid = pingresult.PingResult.GUID;
                int countNum = pingresult.PingResult.DataPending;
                while (countNum > 0)
                {
                    await getnextdataRequest();
                    countNum--;
                    if (correctFlag)
                        break;
                }
            }
            else
            {
                await pingRequest();
            }
        }

        public async Task getnextdataRequest()
        {
            var localStorage = Mvx.Resolve<ILocalStorage>();
            login_Session_Id = localStorage.RetrieveSet("login_session_id");
            customer = localStorage.RetrieveSet("customer");
            string base_url = "http://nxfrontend-qa.nxframework.com/NXRest.svc/getnextdata?customer="+customer+"&session=" + login_Session_Id;
            var getnextdataresult = new GetNextDataInfo();
            getnextdataresult = await APIAccess.APIAccess.GetNextData(base_url);
            if (getnextdataresult.GetNextDataResult.Result == 1)
            {
                guid = getnextdataresult.GetNextDataResult.GUID;
                string dataBase64Str = getnextdataresult.GetNextDataResult.Data;
                string filename = getnextdataresult.GetNextDataResult.Filename;
                if(filename == "consignments.xml")
                {
                    dataXMLStr = Mvx.Resolve<IChangeBase64XML>().GetBase64ToXML(dataBase64Str);
                    correctFlag = true;
                    await setdatacompleteRequest();
                }else
                {
                    correctFlag = false;
                }                
            }
            else
            {
                await setdatafailedRequest();
            }
        }

        public async Task setdatacompleteRequest()
        {
            var localStorage = Mvx.Resolve<ILocalStorage>();
            login_Session_Id = localStorage.RetrieveSet("login_session_id");
            customer = localStorage.RetrieveSet("customer");
            string base_url = "http://nxfrontend-qa.nxframework.com/NXRest.svc/setdatacomplete?customer="+customer+"&session="+ login_Session_Id + "&guid="+guid;
            var setdatacompleteresult = new SetDataCompleteInfo();
            setdatacompleteresult = await APIAccess.APIAccess.SetDataComplete(base_url);
            if (setdatacompleteresult.SetDataCompleteResult.Result == 1)
            {
                Waiting_data = false;
            }
            else
            {
            }
        }

        public async Task setdatafailedRequest()
        {
            var localStorage = Mvx.Resolve<ILocalStorage>();
            login_Session_Id = localStorage.RetrieveSet("login_session_id");
            customer = localStorage.RetrieveSet("customer");
            string base_url = "http://nxfrontend-qa.nxframework.com/NXRest.svc/setdatafailed?customer="+customer+"&session=" + login_Session_Id + "&guid="+guid;
            var setdatafailedresult = new SetDataFailedInfo();
            setdatafailedresult = await APIAccess.APIAccess.SetDataFailed(base_url);
            if (setdatafailedresult.SetDataFailedResult.Result == 1)
            {
                Waiting_data = false;
            }
            else
            {
            }
        }

        public MvxCommand CancelCommand
        {
            get
            {
                return new MvxCommand(() => this.GotoCancel());
            }
        }
        public MvxCommand LinkCommand
        {
            get
            {
                return new MvxCommand(() => this.GotoLink());
            }
        }

        public void GotoCancel()
        {

        }

        public void GotoLink()
        {

        }
    }
}
