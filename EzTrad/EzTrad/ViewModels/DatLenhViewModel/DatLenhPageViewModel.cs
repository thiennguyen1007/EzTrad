using EzTrad.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.DatLenhViewModel
{
    public class DatLenhPageViewModel : BaseViewModel
    {
        private IPageService _pageService;

        //
        private double max { get; set; } = 0;
        private string _opacityValue;
        private string _colorOfLO;
        private string _colorOfATO;
        private string _colorOfPM;
        private string _colorOfATC;
        private string _colorOfPurchase;
        private string _colorOfBtnXacNhan;

        private bool _muaBanValue = true;
        private bool _isShowMenuGia = false;
        private bool _isEnable = false;
        private bool _isEnableGia = false;

        private string _muaBanString;
        private string _txtMa;
        private string _txtKhoiLuong;
        private string _txtGia;
        private string _txtPassWord;
        private bool _statusOfXacNhan;
        private string _stringOfXacNhanBtn;
        private string _lbTienOrCK;
        private string _lbTongTaiSan;
        private string _lbKLMax;
        private double _tongTaiSan;
        private float _tongSoDuCK;
        private MaCompanyViewModel _company;
        //Command
        public ICommand LoadDataCommand { get; private set; }
        public ICommand MuaBanCommand { get; private set; }
        public ICommand LOCommand { get; private set; }
        public ICommand ATOCommand { get; private set; }
        public ICommand MPCommand { get; private set; }
        public ICommand ATCCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand MinusGiaCommand { get; private set; }
        public ICommand PlusGiaCommand { get; private set; }
        public ICommand MinusKhoiLuongCommand { get; private set; }
        public ICommand PlusKhoiLuongCommand { get; private set; }
        public ICommand LbMaxCommand { get; private set; }
        public ICommand BtnTranCommand { get; private set; }
        public ICommand BtnTCCommand { get; private set; }
        public ICommand BtnSanCommand { get; private set; }
        public ICommand BtnMuaCommand { get; private set; }
        public ICommand BtnKhopCommand { get; private set; }
        public ICommand BtnBanCommand { get; private set; }
        public ICommand NaviSoDuCKCommand { get; private set; }
        //
        public string OpacityValue { get => _opacityValue; set => SetProperty(ref _opacityValue, value); }
        public string ColorOfLO { get => _colorOfLO; set => SetProperty(ref _colorOfLO, value); }
        public string ColorOfATO { get => _colorOfATO; set => SetProperty(ref _colorOfATO, value); }
        public string ColorOfMP { get => _colorOfPM; set => SetProperty(ref _colorOfPM, value); }
        public string ColorOfATC { get => _colorOfATC; set => SetProperty(ref _colorOfATC, value); }
        public string ColorOfPurchase { get => _colorOfPurchase; set => SetProperty(ref _colorOfPurchase, value); }
        public string ColorOfBtnXacNhan { get => _colorOfBtnXacNhan; set => SetProperty(ref _colorOfBtnXacNhan, value); }
        public string MuaBanString { get => _muaBanString; set => SetProperty(ref _muaBanString, value); }
        public bool MuaBanValue { get => _muaBanValue; set => SetProperty(ref _muaBanValue, value); }
        public string TxtMa { get => _txtMa; set => SetProperty(ref _txtMa, value); }
        public string TxtKhoiLuong { get => _txtKhoiLuong; set => SetProperty(ref _txtKhoiLuong, value); }
        public string TxtGia { get => _txtGia; set => SetProperty(ref _txtGia, value); }
        public bool StatusOfXacNhan { get => _statusOfXacNhan; set => SetProperty(ref _statusOfXacNhan, value); }
        public string StringOfXacNhanBtn { get => _stringOfXacNhanBtn; set => SetProperty(ref _stringOfXacNhanBtn, value); }
        public MaCompanyViewModel Company { get => _company; set => SetProperty(ref _company, value); }
        public bool IsShowMenuGia { get => _isShowMenuGia; set => SetProperty(ref _isShowMenuGia, value); }
        public string LbTienOrCK { get => _lbTienOrCK; set => SetProperty(ref _lbTienOrCK, value); }
        public double TongTaiSan { get => _tongTaiSan; set => SetProperty(ref _tongTaiSan, value); }
        public string LbTongTaiSan { get => _lbTongTaiSan; set => SetProperty(ref _lbTongTaiSan, value); }
        public float TongSoDuCK { get => _tongSoDuCK; set => SetProperty(ref _tongSoDuCK, value); }
        public string LbKLMax { get => _lbKLMax; set => SetProperty(ref _lbKLMax, value); }
        public bool IsEnable { get => _isEnable; set => SetProperty(ref _isEnable, value); }
        public string TxtPassWord { get => _txtPassWord; set => SetProperty(ref _txtPassWord, value); }
        public bool IsEnableGia { get => _isEnableGia; set => SetProperty(ref _isEnableGia, value); }

        //implement
        public DatLenhPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            TongTaiSan = 1000000000;
            TongSoDuCK = (float)10000;
            //Command
            LoadDataCommand = new Command(LoadData);
            MuaBanCommand = new Command(OnStatusOfMuaBanClicked);
            CancelCommand = new Command(OnCancelClick);
            LOCommand = new Command(OnLOClicked);
            ATOCommand = new Command(OnATOClicked);
            ATCCommand = new Command(OnATCClicked);
            MPCommand = new Command(OnMPClicked);
            LbMaxCommand = new Command(OnLbMaxClicked);
            MinusGiaCommand = new Command(OnMinusGiaClicked);
            PlusGiaCommand = new Command(OnPlusGiaClicked);
            BtnTranCommand = new Command(OnBtnTranClicked);
            BtnTCCommand = new Command(OnBtnTCClicked);
            BtnSanCommand = new Command(OnBtnSanClicked);
            BtnMuaCommand = new Command(OnBtnMuaClicked);
            BtnKhopCommand = new Command(OnBtnKhopClicked);
            BtnBanCommand = new Command(OnBtnBanClicked);
            MinusKhoiLuongCommand = new Command(OnMinusKhoiLuongClicked);
            PlusKhoiLuongCommand = new Command(OnPlusKhoiLuongClicked);
            NaviSoDuCKCommand = new Command(OnSoDuCKClicked);
        }
        private void LoadData()
        {
            ColorOfLO = ColorOfATO = ColorOfMP = ColorOfATC = "White";
            MuaBanString = "Mua";
            ColorOfPurchase = "#007efa";
            ColorOfBtnXacNhan = "#80bdfe";
            StringOfXacNhanBtn = "Xác nhận mua";
            IsEnable = false;
            IsShowMenuGia = false;
            LbTienOrCK = "S.dư tiền";
            LbTongTaiSan = TongTaiSan.ToString();
            StatusOfXacNhan = false;
            Company = new MaCompanyViewModel();
            LbKLMax = null;
        }
        public void SearchChanged(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt) == true || string.IsNullOrEmpty(txt) == true || txt == "")
            {
                Task.Run(() => LoadData());
                return;
            }
            else
            {
                Task.Run(() => LoadData());
                Company = new MaCompanyViewModel();
                Company = GetCompany(txt);
            }
            if (TxtMa.Length >= 3)
            {
                if (Company != null)
                {

                    max = System.Math.Truncate(Convert.ToDouble(TongTaiSan / (Company.PriceSan * 1000)));
                    if (max > Company.KL)
                    {
                        max = Company.KL;
                    }
                    IsShowMenuGia = true;
                    LbKLMax = $"max {max}";
                    Company = GetCompany(txt);
                    IsEnable = true;
                    ColorOfLO = "#fb9807";
                    IsEnableGia = true;
                }
                else
                {
                    Company = new MaCompanyViewModel();
                    _pageService.DisplayAlert("Alert!", "Khong ton tai", "OK");
                    TxtMa = "";
                }
            }

        }
        private void OnSoDuCKClicked()
        {
            _pageService.PushModelAsync(new Views.DatLenhPage.SoDuCKPage());
        }
        public void TxtKhoiLuongChangedCheck(string x)
        {
            if (x != null && x != "")
            {
                TxtKhoiLuong = x;
                double temp = Convert.ToSingle(x) * Company.PriceSan * 1000;

                if (temp > TongTaiSan)
                {
                    _pageService.DisplayAlert("Alert!", "Khong du tien", "OK");
                    TxtKhoiLuong = max.ToString();
                }
                else
                {
                    return;
                }
            }
            CheckIsEnableXacNhan();
        }
        public void TxtPassWordChanged(string x)
        {
            if (TxtPassWord != "")
            {
                CheckIsEnableXacNhan();
            }
        }
        public void TxtGiaChanged(string x)
        {
            if (TxtGia == "" || TxtGia == null)
            {
                return;
            }
            else
            {
                TxtGiaChecked(x);
            }
            CheckIsEnableXacNhan();
        }
        private void TxtGiaChecked(string x)//check xem kl mua * gia co vuot so du tai san hay khong
        {
            if (x != null)
            {
                TxtGia = x;
                float temp = 0;
                if (TxtKhoiLuong != "" && TxtKhoiLuong != "." && TxtGia != "ATC" && TxtGia != "MP" && TxtGia != "ATO")
                {
                    temp = Convert.ToSingle(x) * 1000 * Convert.ToSingle(TxtKhoiLuong);
                    if (temp > TongTaiSan)
                    {
                        _pageService.DisplayAlert("Alert!", "Khong du tien", "OK");
                    }
                }
            }        
        }
        private MaCompanyViewModel GetCompany(string id)
        {
            MaCompanyViewModel result = new MaCompanyViewModel();
            List<MaCompanyViewModel> Temp = new List<MaCompanyViewModel>();
            foreach (var item in MaKhoiTao())
            {
                Temp.Add(item);
            }
            result = Temp.Find(x => x.ID == $"{id}");
            return result;
        }
        private void OnCancelClick()
        {
            TxtMa = "";
            TxtKhoiLuong = "";
            TxtGia = "";
            TxtPassWord = "";
            Company = new MaCompanyViewModel();
        }
        private void OnBtnTranClicked()
        {           
            OnLOClicked();
            TxtGia = Company.PriceTran.ToString();
        }
        private void OnBtnTCClicked()
        {          
            OnLOClicked();
            TxtGia = Company.PriceTC.ToString();
        }
        private void OnBtnSanClicked()
        {      
            OnLOClicked();
            TxtGia = Company.PriceSan.ToString();
        }
        private void OnBtnMuaClicked()
        {           
            OnLOClicked();
            TxtGia = Company.PriceMua.ToString();
        }
        private void OnBtnKhopClicked()
        {           
            OnLOClicked();
            TxtGia = Company.PriceKhop.ToString();
        }
        private void OnBtnBanClicked()
        {           
            OnLOClicked();
            TxtGia = Company.PriceBan.ToString();
        }
        private void OnStatusOfMuaBanClicked()
        {
            MuaBanValue = !MuaBanValue;
            if (MuaBanValue == true)
            {
                MuaBanString = "Mua";
                ColorOfPurchase = "#007efa";
                ColorOfBtnXacNhan = "#007aff";
                StringOfXacNhanBtn = "Xác nhận mua";
                LbTienOrCK = "S.dư tiền";
                LbTongTaiSan = TongTaiSan.ToString();
            }
            else
            {
                ColorOfPurchase = "#c3161c";
                MuaBanString = "Bán";
                ColorOfBtnXacNhan = "#e18b8e";
                StringOfXacNhanBtn = "Xác nhận bán";
                LbTongTaiSan = TongSoDuCK.ToString();
                LbTienOrCK = "Số dư CK";
            }
            CheckIsEnableXacNhan();
        }
        private void OnLbMaxClicked()
        {
            TxtKhoiLuong = max.ToString();
        }
        private void OnMinusKhoiLuongClicked()
        {
            if (TxtKhoiLuong == "" || TxtKhoiLuong == null)
            {
                TxtKhoiLuong = max.ToString();
            }
            else if (Convert.ToSingle(TxtKhoiLuong) < 100)
            {
                TxtKhoiLuong = "0";
            }
            else if (Convert.ToSingle(TxtKhoiLuong) >= 100)
            {
                float tempKL = 0;
                tempKL = Convert.ToSingle(TxtKhoiLuong) - 100;
                TxtKhoiLuong = tempKL.ToString();
            }
            else
            {
                return;
            }
        }
        private void OnPlusKhoiLuongClicked()
        {
            if (TxtKhoiLuong == "" || TxtKhoiLuong == null)
            {
                TxtKhoiLuong = "100";
            }
            else if (Convert.ToSingle(TxtKhoiLuong) < Company.KL)
            {
                float tempKL = 0;
                tempKL = Convert.ToSingle(TxtKhoiLuong) + 100;
                TxtKhoiLuong = tempKL.ToString();
            }
            else if (Convert.ToSingle(TxtKhoiLuong) > Company.KL)
            {
                TxtKhoiLuong = max.ToString();
            }
            else
            {
                return;
            }
        }
        private void OnMinusGiaClicked()
        {
            if (TxtGia == "" || TxtGia == null)
            {
                TxtGia = Company.PriceTran.ToString();
            }
            else if (TxtGia == "ATO" || TxtGia == "MP" || TxtGia == "ATC")
            {
                return;
            }
            else if (Convert.ToSingle(TxtGia) > Company.PriceSan)
            {
                float tempGia = 0;
                tempGia = Convert.ToSingle(TxtGia) - (float)0.1;
                TxtGia = tempGia.ToString();
            }
            else
            {
                return;
            }
        }
        private void OnPlusGiaClicked()
        {
            if (TxtGia == "" || TxtGia == null)
            {
                TxtGia = Company.PriceSan.ToString();
            }
            else if (TxtGia == "ATO" || TxtGia == "MP" || TxtGia == "ATC")
            {
                return;
            }
            else if (Convert.ToSingle(TxtGia) < Company.PriceTran)
            {
                float tempGia = 0;
                tempGia = Convert.ToSingle(TxtGia) + (float)0.1;
                TxtGia = tempGia.ToString();
            }
            else
            {
                return;
            }
        }
        private void OnLOClicked()
        {
            ColorOfLO = "#fb9807";
            TxtGia = "";
            ColorOfATC = ColorOfATO = ColorOfMP = "White";
            IsEnableGia = true;
            StatusOfXacNhan = false;
        }
        private void OnATOClicked()
        {
            ColorOfATO = "#fb9807";
            TxtGia = "ATO";
            ColorOfATC = ColorOfLO = ColorOfMP = "White";
            IsEnableGia = false;
        }
        private void OnMPClicked()
        {
            ColorOfMP = "#fb9807";
            TxtGia = "MP";
            ColorOfATC = ColorOfATO = ColorOfLO = "White";
            IsEnableGia = false;
        }
        private void OnATCClicked()
        {
            ColorOfATC = "#fb9807";
            TxtGia = "ATC";
            IsEnableGia = false;
            ColorOfLO = ColorOfATO = ColorOfMP = "White";
        }
        private void CheckIsEnableXacNhan()
        {
            if (string.IsNullOrEmpty(TxtMa) || string.IsNullOrEmpty(TxtGia) || string.IsNullOrEmpty(TxtKhoiLuong))
            {
                StatusOfXacNhan = false;
            }
            else if (string.IsNullOrWhiteSpace(TxtMa) || string.IsNullOrWhiteSpace(TxtGia) || string.IsNullOrWhiteSpace(TxtKhoiLuong))
            {
                StatusOfXacNhan = false;
            }
            else
            {
                StatusOfXacNhan = true;
            }
            if (StatusOfXacNhan == true && MuaBanValue == true)
            {
                ColorOfBtnXacNhan = "#007aff";
            }
            else if (StatusOfXacNhan == true && MuaBanValue == false)
            { ColorOfBtnXacNhan = "Red"; }
            else if (StatusOfXacNhan == false && MuaBanValue == true)
            {
                ColorOfBtnXacNhan = "#80bdfe";
            }
            else
            {
                ColorOfBtnXacNhan = "#e18b8e";
            }
        }
        public ObservableCollection<MaCompanyViewModel> MaKhoiTao()
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
