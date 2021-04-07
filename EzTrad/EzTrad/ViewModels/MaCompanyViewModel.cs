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
    }
}
