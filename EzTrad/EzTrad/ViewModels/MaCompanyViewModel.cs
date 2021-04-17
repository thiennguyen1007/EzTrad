using System.Collections.ObjectModel;

namespace EzTrad.ViewModels
{
    public class MaCompanyViewModel : BaseViewModel
    {
        private string _iD;
        private string _name;
        private double _priceTran;
        private double _priceTC;
        private double _priceSan;
        private double _priceMua;
        private double _priceKhop;
        private double _priceBan;
        private int _kL;
        public string ID { get => _iD; set => SetProperty(ref _iD, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public double PriceTran { get => _priceTran; set => SetProperty(ref _priceTran, value); }
        public double PriceTC { get => _priceTC; set => SetProperty(ref _priceTC, value); }
        public double PriceSan { get => _priceSan; set => SetProperty(ref _priceSan, value); }
        public double PriceMua { get => _priceMua; set => SetProperty(ref _priceMua, value); }
        public double PriceKhop { get => _priceKhop; set => SetProperty(ref _priceKhop, value); }
        public double PriceBan { get => _priceBan; set => SetProperty(ref _priceBan, value); }
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
                PriceBan = 7,
                PriceKhop = 6.8,
                PriceMua = 6.8,
                PriceSan = 5.8,
                PriceTC = 6.8,
                PriceTran = 7.8,
                KL = 1160,
            };
            MaCompanyViewModel ma1 = new MaCompanyViewModel()
            {
                ID = "FPT",
                Name = "HOSE- Cong ty co phan FPT",
                PriceBan = 79.6,
                PriceKhop = 79.6,
                PriceMua = 79.4,
                PriceSan = 73.9,
                PriceTC = 79.4,
                PriceTran = 84.9,
                KL = 150380,
            };
            MaCompanyViewModel ma2 = new MaCompanyViewModel()
            {
                ID = "CMS",
                Name = "HNX- Cong ty co phan CMVIETNAM",
                PriceBan = 4.7,
                PriceKhop = 4.6,
                PriceMua = 4.6,
                PriceSan = 4,
                PriceTC = 4.4,
                PriceTran = 4.8,
                KL = 15520,
            };
            x.Add(ma);
            x.Add(ma1);
            x.Add(ma2);
            return x;
        }
    }
}
