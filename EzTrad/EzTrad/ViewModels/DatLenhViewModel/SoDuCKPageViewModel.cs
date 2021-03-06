using EzTrad.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.DatLenhViewModel
{
    public class SoDuCKPageViewModel : BaseViewModel
    {
        private string _txtMa;
        private ObservableCollection<MaCompanyViewModel> _lstSoDuCK;
        private IPageService _pageService;
        //Command
        public ICommand ClosePopupCommand { get; private set; }
        //
        public ObservableCollection<MaCompanyViewModel> LstSoDuCK { get => _lstSoDuCK; set => SetProperty(ref _lstSoDuCK, value); }
        public string TxtMa { get => _txtMa; set => SetProperty(ref _txtMa, value); }

        //implement
        public SoDuCKPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            LstSoDuCK = new ObservableCollection<MaCompanyViewModel>(KhoiTaoSoDuCK());
            //Command
            ClosePopupCommand = new Command(OnCloseClicked);
        }       
        private void OnCloseClicked()
        {
            _pageService.PopAsyncPopup();
            MessagingCenter.Send(this, "ReloadData");
        }
        public void SearchCompany()
        {
            if (string.IsNullOrEmpty(TxtMa) || string.IsNullOrWhiteSpace(TxtMa))
            {
                LstSoDuCK = new ObservableCollection<MaCompanyViewModel>(KhoiTaoSoDuCK());
            }
            else
            {
                if (GetCompany(TxtMa) == null)
                {
                    _pageService.DisplayAlert("Alert!", "Khong ton tai ma", "OK");
                    LstSoDuCK = new ObservableCollection<MaCompanyViewModel>(KhoiTaoSoDuCK());
                    TxtMa = null;
                }
                else
                {
                    LstSoDuCK.Clear();
                    LstSoDuCK = new ObservableCollection<MaCompanyViewModel>();
                    LstSoDuCK.Add(GetCompany(TxtMa));
                }
            }
        }
        public void SearchCompanyRealTime(string x)
        {
            if (string.IsNullOrWhiteSpace(TxtMa)|| string.IsNullOrEmpty(TxtMa)|| TxtMa == "")
            {
                LstSoDuCK = new ObservableCollection<MaCompanyViewModel>(KhoiTaoSoDuCK());
            }
            else
            {
                ObservableCollection<MaCompanyViewModel> temp = new ObservableCollection<MaCompanyViewModel>();
                foreach (var item in SearchCompanyLike(x.ToUpper()))
                {
                    temp.Add(item);
                }
                LstSoDuCK = new ObservableCollection<MaCompanyViewModel>(temp);
            }
        }
        private IEnumerable<MaCompanyViewModel> SearchCompanyLike(string newText)
        {
            ObservableCollection<MaCompanyViewModel> temp = new ObservableCollection<MaCompanyViewModel>(KhoiTaoSoDuCK());
            var x = new List<MaCompanyViewModel>(temp);
            return x.Where(c => c.ID != null && c.ID.Contains(newText));
        }
        public void SoDuCKSelected(MaCompanyViewModel x)
        {
            MessagingCenter.Send(this, "Company", x);
            _pageService.PopAsyncPopup();
        }
        private MaCompanyViewModel GetCompany(string id)
        {
            MaCompanyViewModel result = new MaCompanyViewModel();
            List<MaCompanyViewModel> Temp = new List<MaCompanyViewModel>();
            foreach (var item in KhoiTaoSoDuCK())
            {
                Temp.Add(item);
            }
            result = Temp.Find(x => x.ID == $"{id}");
            return result;
        }
        public ObservableCollection<MaCompanyViewModel> KhoiTaoSoDuCK()
        {
            ObservableCollection<MaCompanyViewModel> x = new ObservableCollection<MaCompanyViewModel>();
            MaCompanyViewModel ma = new MaCompanyViewModel()
            {
                ID = "PTV",
                Name = "UPCOM- Cong ty co phan thuong mai Dau Khi",
                KL = 200,
            };
            MaCompanyViewModel ma1 = new MaCompanyViewModel()
            {
                ID = "FPT",
                Name = "HOSE- Cong ty co phan FPT",
                KL = 1200,
            };
            MaCompanyViewModel ma2 = new MaCompanyViewModel()
            {
                ID = "CMS",
                Name = "HNX- Cong ty co phan CMVIETNAM",
                KL = 300,
            };
            x.Add(ma);
            x.Add(ma1);
            x.Add(ma2);
            return x;
        }
    }
}
