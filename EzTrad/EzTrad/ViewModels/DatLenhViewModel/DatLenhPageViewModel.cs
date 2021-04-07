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
        private string _opacityValue;
        private string _colorOfLO;
        private string _colorOfATO;
        private string _colorOfPM;
        private string _colorOfATC;
        private string _colorOfPurchase;
        private string _colorOfBtnXacNhan;

        private bool _muaBanValue = true;
        private bool _isShowMenuGia = false;

        private string _muaBanString;
        private string _txtMa;
        private string _txtKhoiLuong;
        private string _txtGia;
        private string _statusOfXacNhan;
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
        public string StatusOfXacNhan { get => _statusOfXacNhan; set => SetProperty(ref _statusOfXacNhan, value); }
        public string StringOfXacNhanBtn { get => _stringOfXacNhanBtn; set => SetProperty(ref _stringOfXacNhanBtn, value); }
        public MaCompanyViewModel Company { get => _company; set => SetProperty(ref _company, value); }
        public bool IsShowMenuGia { get => _isShowMenuGia; set => SetProperty(ref _isShowMenuGia, value); }
        public string LbTienOrCK { get => _lbTienOrCK; set => SetProperty(ref _lbTienOrCK, value); }
        public double TongTaiSan { get => _tongTaiSan; set => SetProperty(ref _tongTaiSan, value); }
        public string LbTongTaiSan { get => _lbTongTaiSan; set => SetProperty(ref _lbTongTaiSan, value); }
        public float TongSoDuCK { get => _tongSoDuCK; set => SetProperty(ref _tongSoDuCK, value); }
        public string LbKLMax { get => _lbKLMax; set => SetProperty(ref _lbKLMax, value); }

        //implement
        public DatLenhPageViewModel()
        {
            TongTaiSan =  1000000000;
            TongSoDuCK = (float)10000;
            //Command
            LoadDataCommand = new Command(LoadData);
            MuaBanCommand = new Command(OnStatusOfMuaBanClicked);
            CancelCommand = new Command(OnCancelClick);
            LOCommand = new Command(OnLOClicked);
            ATOCommand = new Command(OnATOClicked);
            ATCCommand = new Command(OnATCClicked);
            MPCommand = new Command(OnMPClicked);
        }
        private void LoadData()
        {
            ColorOfLO = ColorOfATO = ColorOfMP = ColorOfATC = "White";
            MuaBanString = "Mua";
            ColorOfPurchase = "#007efa";
            ColorOfBtnXacNhan = "#007aff";
            StringOfXacNhanBtn = "Xác nhận mua";
            IsShowMenuGia = false;
            LbTienOrCK = "S.dư tiền";
            LbTongTaiSan = TongTaiSan.ToString();
            Company = new MaCompanyViewModel();
            Company.PriceBan = 0;
            Company.PriceKhop = 0;
            Company.PriceMua = 0;
            Company.PriceSan = 0;
            Company.PriceTC = 0;
            Company.PriceTran = 0;
            LbKLMax = "max 0";
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
                Company=new MaCompanyViewModel();
                Company = GetCompany(txt);
            }
            if (Company != null)
            {
                int max = 0;
                max = Convert.ToInt32(TongTaiSan/Company.PriceSan);
                if (max > Company.KL)
                {
                    max = Company.KL;
                }
                IsShowMenuGia = true;
                LbKLMax = $"max {max}";
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
            result = Temp.Find(x=> x.ID==$"{id}");
            return result;
        }
        private void OnCancelClick()
        {
            TxtMa = "";
            TxtKhoiLuong = "";
            TxtGia = "";
            Company = new MaCompanyViewModel();
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
        private void OnMinusGiaClicked()
        {
            if (TxtGia == "" || TxtGia == null)
            {
                TxtGia = Company.PriceTran.ToString();
            }
            else if(Convert.ToSingle(TxtGia) > Company.PriceSan)
            {
                
            }
        }
        private void OnLOClicked()
        {
            ColorOfLO = "#fb9807";
            TxtGia = "";
            ColorOfATC = ColorOfATO = ColorOfMP = "White";
        }
        private void OnATOClicked()
        {
            ColorOfATO = "#fb9807";
            TxtGia = "ATO";
            ColorOfATC = ColorOfLO = ColorOfMP = "White";
        }
        private void OnMPClicked()
        {
            ColorOfMP = "#fb9807";
            TxtGia = "MP";
            ColorOfATC = ColorOfATO = ColorOfLO = "White";
        }
        private void OnATCClicked()
        {
            ColorOfATC = "#fb9807";
            TxtGia = "ATC";
            ColorOfLO = ColorOfATO = ColorOfMP = "White";
        }
        private void CheckIsEnableXacNhan()
        {
            if (string.IsNullOrEmpty(TxtMa) || string.IsNullOrEmpty(TxtGia) || string.IsNullOrEmpty(TxtKhoiLuong))
            {
                StatusOfXacNhan = "False";
            }
            else if (string.IsNullOrWhiteSpace(TxtMa) || string.IsNullOrWhiteSpace(TxtGia) || string.IsNullOrWhiteSpace(TxtKhoiLuong))
            {
                StatusOfXacNhan = "False";
            }
            else
            {
                StatusOfXacNhan = "True";
            }
            if (StatusOfXacNhan == "True" && MuaBanValue == true)
            {
                ColorOfBtnXacNhan = "#007aff";
            }
            else if (StatusOfXacNhan == "True" && MuaBanValue == false)
            { ColorOfBtnXacNhan = "Red"; }
            else if (StatusOfXacNhan == "False" && MuaBanValue == true)
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
                PriceKhop =(float)79.6,
                PriceMua =(float)79.4,
                PriceSan =(float)73.9,
                PriceTC =(float)79.4,
                PriceTran =(float)84.9,
                KL=150380,
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
        