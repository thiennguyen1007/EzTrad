using System.Collections.ObjectModel;

namespace EzTrad.ViewModels
{
    public class MaCompanyViewModel : BaseViewModel
    {
        private string _iD;
        private string _name;
        private float _priceTran;
        private float _priceTC;
        private float _priceSan;
        private float _priceMua;
        private float _priceKhop;
        private float _priceBan;
        private int _kL;
        public string ID { get => _iD; set => SetProperty(ref _iD, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public float PriceTran { get => _priceTran; set => SetProperty(ref _priceTran, value); }
        public float PriceTC { get => _priceTC; set => SetProperty(ref _priceTC, value); }
        public float PriceSan { get => _priceSan; set => SetProperty(ref _priceSan, value); }
        public float PriceMua { get => _priceMua; set => SetProperty(ref _priceMua, value); }
        public float PriceKhop { get => _priceKhop; set => SetProperty(ref _priceKhop, value); }
        public float PriceBan { get => _priceBan; set => SetProperty(ref _priceBan, value); }
        public int KL { get => _kL; set => SetProperty(ref _kL, value); }
        public MaCompanyViewModel(MaCompanyViewModel x)
        {
            ID = x.ID;
            Name = x.Name;
            KL = x.KL;
            PriceBan = x.PriceBan;
            PriceKhop = x.PriceKhop;
            PriceMua = x.PriceMua;
            PriceSan = x.PriceSan;
            PriceTC = x.PriceTC;
            PriceTran = x.PriceTran;
        }
        public MaCompanyViewModel() { }
        public ObservableCollection<MaCompanyViewModel> GetAllCompany()
        {
            ObservableCollection<MaCompanyViewModel> x = new ObservableCollection<MaCompanyViewModel>();
            MaCompanyViewModel ma = new MaCompanyViewModel()
            {
                ID = "PTV",
                Name = "UPCOM- Cong ty co phan thuong mai Dau Khi",
                PriceBan = (float)7,
                PriceKhop = (float)6.8,
                PriceMua = (float)6.8,
                PriceSan = (float)5.8,
                PriceTC = (float)6.8,
                PriceTran = (float)7.8,
                KL = 1160,
            };
            MaCompanyViewModel ma1 = new MaCompanyViewModel()
            {
                ID = "FPT",
                Name = "HOSE- Cong ty co phan FPT",
                PriceBan = (float)79.6,
                PriceKhop = (float)79.6,
                PriceMua = (float)79.4,
                PriceSan = (float)73.9,
                PriceTC = (float)79.4,
                PriceTran = (float)84.9,
                KL = 150380,
            };
            MaCompanyViewModel ma2 = new MaCompanyViewModel()
            {
                ID = "CMS",
                Name = "HNX- Cong ty co phan CMVIETNAM",
                PriceBan = (float)4.7,
                PriceKhop = (float)4.6,
                PriceMua = (float)4.6,
                PriceSan = (float)4,
                PriceTC = (float)4.4,
                PriceTran = (float)4.8,
                KL = 15520,
            };
            x.Add(ma);
            x.Add(ma1);
            x.Add(ma2);
            return x;
        }
    }
}
