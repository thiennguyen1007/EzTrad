using EzTrad.Services;
using EzTrad.Views.DatLenhPage;
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
        private string _lbLoaiGD;
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
        // Company in Stock exchanges
        private MaCompanyViewModel _company;
        //Company user have stock
        private MaCompanyViewModel _companyHaveCK;
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
        public ICommand LoaiGDCommand { get; private set; }
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
        public string LbLoaiGD { get => _lbLoaiGD; set => SetProperty(ref _lbLoaiGD, value); }
        public MaCompanyViewModel CompanyHaveCK { get => _companyHaveCK; set => SetProperty(ref _companyHaveCK, value); }

        //implement
        public DatLenhPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            TongTaiSan = 1000000000;
            TongSoDuCK = 1750;
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
            LoaiGDCommand = new Command(OnLoaiGDClicked);
            //
            MessagingCenter.Subscribe<SoDuCKPageViewModel, MaCompanyViewModel>(this, "Company", SubcribeCompanyAndUpdate);
        }
        private void SubcribeCompanyAndUpdate(SoDuCKPageViewModel sender, MaCompanyViewModel companySent)
        {
            Company = new MaCompanyViewModel();
            MuaBanValue = false;
            //
            Company = companySent;
            TxtMa = companySent.ID.ToString();
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void LoadData()
        {
            //khoi tao
            Company = new MaCompanyViewModel();
            CompanyHaveCK = new MaCompanyViewModel();
            //
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
            LbKLMax = null;
            LbLoaiGD = "Thường";
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
                    if (MuaBanValue == true)
                    {
                        max = System.Math.Truncate(Convert.ToDouble(TongTaiSan / (Company.PriceSan * 1000)));
                        if (max > Company.KL)
                        {
                            max = Company.KL;
                        }
                    }
                    else
                    {
                        max = GetCompanyInYourSecuritiesWallet(Company.ID).KL;
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
            CheckIsEnableXacNhan();
        }
        private async void OnSoDuCKClicked()
        {
            await _pageService.PushModelAsync(new SoDuCKPage());
        }
        public void TxtKhoiLuongChangedCheck()
        {
            if (TxtKhoiLuong != null && TxtKhoiLuong != "")
            {
                if (MuaBanValue == true)
                {
                    double temp = Convert.ToSingle(TxtKhoiLuong) * Company.PriceSan * 1000;

                    if (temp > TongTaiSan)
                    {
                        _pageService.DisplayAlert("Alert!", "Khong du tien", "OK");
                        TxtKhoiLuong = max.ToString();
                    }
                    else if (TxtKhoiLuong == "0" || Convert.ToSingle(TxtKhoiLuong) < 100)
                    {
                        _pageService.DisplayAlert("Alert!", "Khoi luong >=100", "OK");
                        TxtKhoiLuong = "";
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (Convert.ToSingle(TxtKhoiLuong) > Company.KL)
                    {
                        _pageService.DisplayAlert("Alert!", "Ví chứng khoán không đủ", "OK");
                        TxtKhoiLuong = max.ToString();
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        public void TxtPassWordChanged(string x)
        {
            CheckIsEnableXacNhan();
        }
        public void TxtGiaChanged()
        {
            if (TxtGia == "" || TxtGia == null)
            {
                return;
            }
            else
            {
                TxtGiaChecked();
            }
            CheckIsEnableXacNhan();
        }
        private void TxtGiaChecked()//check xem kl mua * gia co vuot so du tai san hay khong
        {
            if (TxtGia != null)
            {
                float temp = 0;
                if (TxtKhoiLuong != "" && TxtKhoiLuong != "." && TxtGia != "ATC" && TxtGia != "MP" && TxtGia != "ATO")
                {
                    temp = Convert.ToSingle(TxtGia) * 1000 * Convert.ToSingle(TxtKhoiLuong);
                    if (temp > TongTaiSan)
                    {
                        _pageService.DisplayAlert("Alert!", "Khong du tien", "OK");
                        TxtGia = "";
                    }
                    else if (temp == 0 || Convert.ToSingle(TxtGia) < Company.PriceSan)
                    {
                        _pageService.DisplayAlert("Alert!", "Gia thap hon gia san", "OK");
                        TxtGia = "";
                    }
                }
            }
        }
        private void OnLoaiGDClicked()
        {
            if (LbLoaiGD == "Thường")
            {
                LbLoaiGD = "Mergin";
                MuaBanValue = false;
                Task.Delay(100);
                _pageService.PushModelAsync(new SoDuCKPage());
            }
            else
            { LbLoaiGD = "Thường"; }
            CheckMuaBan();
            CheckIsEnableXacNhan();
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
            CheckIsEnableXacNhan();
        }
        private void OnBtnTCClicked()
        {
            OnLOClicked();
            TxtGia = Company.PriceTC.ToString();
            CheckIsEnableXacNhan();
        }
        private void OnBtnSanClicked()
        {
            OnLOClicked();
            TxtGia = Company.PriceSan.ToString();
            CheckIsEnableXacNhan();

        }
        private void OnBtnMuaClicked()
        {
            OnLOClicked();
            TxtGia = Company.PriceMua.ToString();
            CheckIsEnableXacNhan();

        }
        private void OnBtnKhopClicked()
        {
            OnLOClicked();
            TxtGia = Company.PriceKhop.ToString();
            CheckIsEnableXacNhan();

        }
        private void OnBtnBanClicked()
        {
            OnLOClicked();
            TxtGia = Company.PriceBan.ToString();
            CheckIsEnableXacNhan();
        }
        private void OnStatusOfMuaBanClicked()
        {
            MuaBanValue = !MuaBanValue;

            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        public void CheckMuaBan()
        {
            if (MuaBanValue == true)
            {
                MuaBanString = "Mua";
                LbTienOrCK = "S.dư tiền";
                //color
                ColorOfPurchase = "#007efa";
                ColorOfBtnXacNhan = "#80bdfe";
                StringOfXacNhanBtn = "Xác nhận mua";
                LbTongTaiSan = TongTaiSan.ToString();
            }
            else
            {
                MuaBanString = "Bán";
                LbTienOrCK = "Số dư CK";
                //color
                ColorOfPurchase = "#c3161c";
                ColorOfBtnXacNhan = "#e18b8e";
                StringOfXacNhanBtn = "Xác nhận bán";
                LbTongTaiSan = TongSoDuCK.ToString();
            }
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
                _pageService.DisplayAlert("Alert!", "Khoi luong min= 100", "Ok");
                TxtKhoiLuong = "100";
            }
            else if (Convert.ToSingle(TxtKhoiLuong) >= 100)
            {
                float tempKL = 0;
                tempKL = Convert.ToSingle(TxtKhoiLuong) - 100;
                if (tempKL < 100)
                {
                    _pageService.DisplayAlert("Alert!", "Khoi luong min= 100", "Ok");
                    TxtKhoiLuong = "100";
                }
                TxtKhoiLuong = tempKL.ToString();
            }
            else
            {
                return;
            }
            CheckIsEnableXacNhan();
        }
        private void OnPlusKhoiLuongClicked()
        {
            if (MuaBanValue == true)
            {
                if (TxtKhoiLuong == "" || TxtKhoiLuong == null)
                {
                    TxtKhoiLuong = "100";
                }
                else if (Convert.ToSingle(TxtKhoiLuong) < max)
                {
                    float tempKL = 0;
                    tempKL = Convert.ToSingle(TxtKhoiLuong) + 100;
                    TxtKhoiLuong = tempKL.ToString();
                }
                else if (Convert.ToSingle(TxtKhoiLuong) > Company.KL)
                {
                    _pageService.DisplayAlert("Alert!", "Khoi luong vuot muc", "Ok");
                    TxtKhoiLuong = max.ToString();
                }
                else
                {
                    return;
                }
            }
            else
            {
                float tempStockHad = 0;
                //tim kiem cong ty trong ví chứng khoán của người dùng sở hữu
                tempStockHad = GetCompanyInYourSecuritiesWallet(Company.ID).KL;
                if (TxtKhoiLuong == "" || TxtKhoiLuong == null)
                {
                    TxtKhoiLuong = "100";
                }
                else if (Convert.ToSingle(TxtKhoiLuong) <= tempStockHad)
                {
                    float tempKL = 0;
                    tempKL = Convert.ToSingle(TxtKhoiLuong) + 100;
                    if (tempKL > tempStockHad)
                    {
                        _pageService.DisplayAlert("Alert!", "Ví chứng khoán không đủ", "OK");
                        tempKL = tempStockHad;
                    }
                    TxtKhoiLuong = tempKL.ToString();
                }
                else if (Convert.ToSingle(TxtKhoiLuong) > Company.KL)
                {
                    _pageService.DisplayAlert("Alert!", "Khoi luong vuot muc", "Ok");
                    TxtKhoiLuong = max.ToString();
                }
                else
                {
                    return;
                }
            }
            CheckIsEnableXacNhan();
        }
        private MaCompanyViewModel GetCompanyInYourSecuritiesWallet(string x)
        {
            SoDuCKPageViewModel userStocck = new SoDuCKPageViewModel(null);
            ObservableCollection<MaCompanyViewModel> lstTemp = new ObservableCollection<MaCompanyViewModel>();
            lstTemp = userStocck.KhoiTaoSoDuCK();
            //tim kiem cong ty trong ví chứng khoán của người dùng sở hữu
            for (int i = 0; i < lstTemp.Count; i++)
            {

                if (Company.ID == lstTemp[i].ID)
                {
                    CompanyHaveCK = new MaCompanyViewModel(lstTemp[i]);
                }
            }
            return CompanyHaveCK;
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
            CheckIsEnableXacNhan();
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
            CheckIsEnableXacNhan();
        }
        private void OnLOClicked()
        {
            ColorOfLO = "#fb9807";
            TxtGia = "";
            ColorOfATC = ColorOfATO = ColorOfMP = "White";
            IsEnableGia = true;
            StatusOfXacNhan = false;
            CheckIsEnableXacNhan();
        }
        private void OnATOClicked()
        {
            ColorOfATO = "#fb9807";
            TxtGia = "ATO";
            ColorOfATC = ColorOfLO = ColorOfMP = "White";
            IsEnableGia = false;
            CheckIsEnableXacNhan();
        }
        private void OnMPClicked()
        {
            ColorOfMP = "#fb9807";
            TxtGia = "MP";
            ColorOfATC = ColorOfATO = ColorOfLO = "White";
            IsEnableGia = false;
            CheckIsEnableXacNhan();
        }
        private void OnATCClicked()
        {
            ColorOfATC = "#fb9807";
            TxtGia = "ATC";
            IsEnableGia = false;
            ColorOfLO = ColorOfATO = ColorOfMP = "White";
            CheckIsEnableXacNhan();
        }
        private void CheckIsEnableXacNhan()
        {
            if (string.IsNullOrEmpty(TxtMa) || string.IsNullOrEmpty(TxtGia) || string.IsNullOrEmpty(TxtKhoiLuong) || string.IsNullOrEmpty(TxtPassWord))
            {
                StatusOfXacNhan = false;
            }
            else if (string.IsNullOrWhiteSpace(TxtMa) || string.IsNullOrWhiteSpace(TxtGia) || string.IsNullOrWhiteSpace(TxtKhoiLuong) || string.IsNullOrWhiteSpace(TxtPassWord))
            {
                StatusOfXacNhan = false;
            }
            else if (TxtMa != "" && TxtPassWord != "" && TxtKhoiLuong != "" && TxtGia != "")
            {
                StatusOfXacNhan = true;
            }
            else
            {
                StatusOfXacNhan = false;
            }
            //
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
            return Company.GetAllCompany();
        }
    }
}
