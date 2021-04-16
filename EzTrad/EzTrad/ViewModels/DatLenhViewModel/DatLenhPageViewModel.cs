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
            MessagingCenter.Subscribe<SoDuCKPageViewModel>(this, "ReloadData", ClosePopupSoDu);
            //
            MessagingCenter.Unsubscribe<DatLenhPageViewModel>(this, "ReloadData");
            MessagingCenter.Unsubscribe<DatLenhPageViewModel>(this, "Company");
        }

        private void ClosePopupSoDu(SoDuCKPageViewModel obj)
        {
            MuaBanValue = false;
            LbLoaiGD = "Margin";
            TxtKhoiLuong = null;
            TxtGia = null;
            CheckIDAfterUnfocus();
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void SubcribeCompanyAndUpdate(SoDuCKPageViewModel sender, MaCompanyViewModel companySent)
        {
            Company = new MaCompanyViewModel();
            MuaBanValue = false;
            //
            Company = companySent;
            TxtMa = companySent.ID.ToString();
            TxtKhoiLuong = companySent.KL.ToString();
            LbLoaiGD = "Thường";
            CheckIDAfterUnfocus();
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void LoadData()
        {
            //khoi tao
            Company = new MaCompanyViewModel();
            CompanyHaveCK = new MaCompanyViewModel();
            //
            TxtMa = null;
            ColorOfLO = ColorOfATO = ColorOfMP = ColorOfATC = "White";
            MuaBanString = "Mua";
            ColorOfBtnXacNhan = "#80bdfe";
            StringOfXacNhanBtn = "Xác nhận mua";
            IsEnableGia = false;
            IsEnable = false;
            IsShowMenuGia = false;
            TxtGia = TxtKhoiLuong = null;
            LbTienOrCK = "S.dư tiền";
            LbTongTaiSan = TongTaiSan.ToString();
            StatusOfXacNhan = false;
            LbKLMax = null;
            LbLoaiGD = "Thường";
        }
        public void SearchIdRealTime(string realTimeTextID)
        {
            if (string.IsNullOrWhiteSpace(realTimeTextID) == true || string.IsNullOrEmpty(realTimeTextID) == true || realTimeTextID == "")
            {
                return;
            }
            else
            {
                if (GetCompany(realTimeTextID) != null)
                {
                    Company = new MaCompanyViewModel(GetCompany(realTimeTextID.ToUpper()));
                    IsShowMenuGia = true;
                    IsEnable = true;
                    ColorOfLO = "#fb9807";
                    IsEnableGia = true;
                }
                else
                {
                    return;
                }
            }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        public void CheckIDAfterUnfocus()
        {
            if (TxtMa == null)
            { return; }
            else
            {
                if (Company.ID == null)
                {
                    Company = new MaCompanyViewModel();
                    _pageService.DisplayAlert("Alert!", "Khong ton tai", "OK");
                    MessagingCenter.Send(this, "foucusID");
                    TxtMa = null;
                }
            }
        }
        private async void OnSoDuCKClicked()
        {
            LbLoaiGD = "Thường";
            await _pageService.PushModelAsync(new SoDuCKPage());
        }
        public void TxtKhoiLuongUnfocus()
        {
            if (TxtKhoiLuong != null && TxtKhoiLuong != "")
            {
                if (IsValidKL() == true)
                {
                    if (MuaBanValue == true)
                    {
                        double tempTong = Convert.ToSingle(TxtKhoiLuong) * Company.PriceSan * 1000;
                        if (tempTong > TongTaiSan)
                        {
                            _pageService.DisplayAlert("Alert!", "khong du tien", "OK");
                            MessagingCenter.Send(this, "foucusKhoiLuong");
                            TxtKhoiLuong = max.ToString();
                        }
                    }
                    else
                    {
                        if (Convert.ToSingle(TxtKhoiLuong) > Company.KL)
                        {
                            _pageService.DisplayAlert("Alert!", "Ví chứng khoán không đủ", "OK");
                            MessagingCenter.Send(this, "foucusKhoiLuong");
                            TxtKhoiLuong = max.ToString();
                        }
                    }
                }
            }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        public void TxtPassWordChanged(string x)
        {
            CheckIsEnableXacNhan();
        }
        public void TxtGiaChanged()
        {
            if (TxtGia != null)
            {
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == false)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
                if (Convert.ToSingle(TxtGia) < Company.PriceSan)
                {
                    _pageService.DisplayAlert("Alert!", "Gia thap hon gia san", "ok");
                    MessagingCenter.Send(this, "foucusGia");
                    TxtGia = null;
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnLoaiGDClicked()
        {
            if (LbLoaiGD == "Thường")
            {
                LbLoaiGD = "Margin";
                if (MuaBanValue == false)
                {
                    _pageService.PushModelAsync(new SoDuCKPage());
                }
            }
            else
            { LbLoaiGD = "Thường"; }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void OnCancelClick()
        {
            //TxtMa = null;
            //MuaBanValue = true;
            //TxtKhoiLuong = null;
            //IsEnable = false;
            //IsEnableGia = false;
            //TxtGia = null;
            //TxtPassWord = null;
            //Company = new MaCompanyViewModel();
            //CheckMuaBan();
            //CheckIsEnableXacNhan();
            LoadData();
        }
        //btn
        private void OnBtnTranClicked()
        {
            OnLOClicked();
            if (Company.PriceTran == 0)
            {
                IsEnableGia = false;
            }
            else
            {
                TxtGia = Company.PriceTran.ToString();
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == false)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnBtnTCClicked()
        {
            OnLOClicked();
            if (Company.PriceTC == 0)
            {
                IsEnableGia = false;
            }
            else
            {
                TxtGia = Company.PriceTC.ToString();
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == false)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnBtnSanClicked()
        {
            OnLOClicked();
            if (Company.PriceSan == 0)
            {
                IsEnableGia = false;
            }
            else
            {
                TxtGia = Company.PriceSan.ToString();
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == false)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnBtnMuaClicked()
        {
            OnLOClicked();
            if (Company.PriceMua == 0)
            { IsEnableGia = false; }
            else
            {
                TxtGia = Company.PriceMua.ToString();
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == false)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnBtnKhopClicked()
        {
            OnLOClicked();
            if (Company.PriceKhop == 0)
            { IsEnableGia = false; }
            else
            {
                TxtGia = Company.PriceKhop.ToString();
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == false)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnBtnBanClicked()
        {
            OnLOClicked();
            if (Company.PriceBan == 0)
            {
                IsEnableGia = false;
            }
            else
            {
                TxtGia = Company.PriceBan.ToString();
                if (TxtKhoiLuong != null)
                {
                    if (IsValidTaiSan() == true)
                    {
                        MessagingCenter.Send(this, "foucusGia");
                        TxtGia = null;
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        //
        private void OnStatusOfMuaBanClicked()
        {
            MuaBanValue = !MuaBanValue;
            if (MuaBanValue == false && LbLoaiGD=="Margin")
            {
                _pageService.PushModelAsync(new SoDuCKPage());
            }
            TxtKhoiLuong = null;
            TxtGia = null;
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void OnLbMaxClicked()
        {
            TxtKhoiLuong = max.ToString();
            if (TxtGia != null)
            {
                if (IsValidTaiSan() == false)
                {
                    MessagingCenter.Send(this, "foucusKhoiLuong");
                    TxtKhoiLuong = null;
                }
            }
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
        private bool IsValidKL()
        {
            double temp = Convert.ToSingle(TxtKhoiLuong);
            if (temp < 100)
            {
                _pageService.DisplayAlert("Alert!", "Khoi luong >=100", "OK");
                MessagingCenter.Send(this, "foucusKhoiLuong");
                TxtKhoiLuong = null;
                return false;
            }
            else if (temp > max)
            {
                _pageService.DisplayAlert("Alert!", "Vuot Nguong", "OK");
                MessagingCenter.Send(this, "foucusKhoiLuong");
                TxtKhoiLuong = null;
                return false;
            }
            else
            { return true; }
        }
        private void CheckMuaBan()
        {
            if (MuaBanValue == true)
            {
                MuaBanString = "Mua";
                LbTienOrCK = "S.dư tiền";
                if (TxtMa != null)
                {
                    //Company = GetCompany(TxtMa);
                    max = System.Math.Truncate(Convert.ToDouble(TongTaiSan / (Company.PriceSan * 1000)));
                    if (max > Company.KL)
                    {
                        max = Company.KL;
                    }
                }
                //color
                ColorOfBtnXacNhan = "#80bdfe";
                StringOfXacNhanBtn = "Xác nhận mua";
                LbTongTaiSan = TongTaiSan.ToString();
            }
            else
            {
                MuaBanString = "Bán";
                LbTienOrCK = "Số dư CK";
                //color
                ColorOfBtnXacNhan = "#e18b8e";
                StringOfXacNhanBtn = "Xác nhận bán";
                LbTongTaiSan = TongSoDuCK.ToString();
                if (TxtMa != null)
                {
                    max = GetCompanyInYourSecuritiesWallet(Company.ID).KL;
                }
            }
            LbKLMax = $"max {max}";
        }
        private bool IsValidTaiSan()
        {
            bool result = true;
            double Gia = Convert.ToDouble(TxtGia);
            double KL = Convert.ToDouble(TxtKhoiLuong);
            double temp = Gia * KL * 1000;
            if (TxtKhoiLuong != "" && TxtKhoiLuong != "." && TxtGia != "ATC" && TxtGia != "MP" && TxtGia != "ATO")
            {
                if (temp > TongTaiSan)
                {
                    _pageService.DisplayAlert("Alert!", "Khong du tien", "OK");
                    result = false;
                }
            }
            return result;
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
        public ObservableCollection<MaCompanyViewModel> MaKhoiTao()
        {
            return Company.GetAllCompany();
        }
    }
}
