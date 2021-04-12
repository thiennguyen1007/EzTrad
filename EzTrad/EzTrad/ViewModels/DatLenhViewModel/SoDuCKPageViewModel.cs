using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.DatLenhViewModel
{
    
    public class SoDuCKPageViewModel : BaseViewModel
    {
        private ObservableCollection<MaCompanyViewModel> _listSoDuCK;
        public ICommand LoadDataCommand { get; set; }
        public SoDuCKPageViewModel()
        {
            LoadDataCommand = new Command(LoadData);
        }

        public ObservableCollection<MaCompanyViewModel> ListSoDuCK { get => _listSoDuCK; set => SetProperty(ref _listSoDuCK, value); }
        private void LoadData()
        {
            ListSoDuCK = new ObservableCollection<MaCompanyViewModel>(KhoiTaoSoDuCK());
        }
        private ObservableCollection<MaCompanyViewModel> KhoiTaoSoDuCK()
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
                KL = 1250,
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
