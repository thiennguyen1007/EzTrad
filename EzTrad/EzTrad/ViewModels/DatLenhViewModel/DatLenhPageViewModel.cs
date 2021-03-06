using EzTrad.Services;
using EzTrad.Views.DatLenhPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.DatLenhViewModel
{
    public class DatLenhPageViewModel : BaseViewModel
    {
        private IPageService _pageService;
        //
        private double max { get; set; } = 0;
        //private string _colorOfLO;
        private string _colorOfBtnXacNhan;

        private bool _muaBanValue = true;
        private bool _isEnable = false;
        private bool _isEnableGia = false;
        private bool _isEnableSoDuCK = true;

        private string _muaBanString;
        private string _lbLoaiGD;
        private string _txtMa;
        private string _txtKhoiLuong;
        private string _txtGia;
        private string _txtPassWord;
        private string _lbTienOrCK;
        private string _lbTongTaiSan;
        private string _lbKLMax;
        public double TongTaiSan { get; set; }
        public double TongSoDuCK { get; set; }
        // Company in Stock exchanges
        private MaCompanyViewModel _company;
        //Company user have stock
        private MaCompanyViewModel _companyHaveCK;
        //Command
        public ICommand LoadDataCommand { get; private set; }
        public ICommand MuaBanCommand { get; private set; }
        public ICommand LoaiGiaCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand GiaCommand { get; private set; }
        public ICommand KhoiLuongCommand { get; private set; }
        public ICommand LbMaxCommand { get; private set; }
        public ICommand FillGiaCommand { get; private set; }
        public ICommand NaviSoDuCKCommand { get; private set; }
        public ICommand LoaiGDCommand { get; private set; }
        //
        public string ColorOfBtnXacNhan { get => _colorOfBtnXacNhan; set => SetProperty(ref _colorOfBtnXacNhan, value); }
        public string MuaBanString { get => _muaBanString; set => SetProperty(ref _muaBanString, value); }
        public bool MuaBanValue { get => _muaBanValue; set => SetProperty(ref _muaBanValue, value); }
        public string TxtMa { get => _txtMa; set => SetProperty(ref _txtMa, value); }
        public string TxtKhoiLuong { get => _txtKhoiLuong; set => SetProperty(ref _txtKhoiLuong, value); }
        public string TxtGia { get => _txtGia; set => SetProperty(ref _txtGia, value); }
        public MaCompanyViewModel Company { get => _company; set => SetProperty(ref _company, value); }
        public string LbTienOrCK { get => _lbTienOrCK; set => SetProperty(ref _lbTienOrCK, value); }
        public string LbTongTaiSan { get => _lbTongTaiSan; set => SetProperty(ref _lbTongTaiSan, value); }
        public string LbKLMax { get => _lbKLMax; set => SetProperty(ref _lbKLMax, value); }
        public bool IsEnable { get => _isEnable; set => SetProperty(ref _isEnable, value); }
        public string TxtPassWord { get => _txtPassWord; set => SetProperty(ref _txtPassWord, value); }
        public bool IsEnableGia { get => _isEnableGia; set => SetProperty(ref _isEnableGia, value); }
        public string LbLoaiGD { get => _lbLoaiGD; set => SetProperty(ref _lbLoaiGD, value); }
        public MaCompanyViewModel CompanyHaveCK { get => _companyHaveCK; set => SetProperty(ref _companyHaveCK, value); }
        public bool IsEnableSoDuCK { get => _isEnableSoDuCK; set => SetProperty(ref _isEnableSoDuCK, value); }

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
            LoaiGiaCommand = new Command<string>(OnLoaiGiaClicked);
            LbMaxCommand = new Command(OnLbMaxClicked);
            GiaCommand = new Command<string>(OnGiaClicked);
            FillGiaCommand = new Command<string>(OnFillGiaClicked);
            KhoiLuongCommand = new Command<string>(OnKhoiLuongClicked);
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
            if (LbLoaiGD.Equals("Margin"))
            {
                LbLoaiGD = "Thường";
            }
            TxtGia = TxtKhoiLuong = null;
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
            TxtGia = null;
            LbLoaiGD = "Thường";
            SearchIdRealTime(TxtMa);
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
            LbTongTaiSan = TongTaiSan.ToString();
            MuaBanValue = true;
            TxtGia = TxtKhoiLuong = LbKLMax = TxtMa = null;
            IsEnable = IsEnableGia = false;
            ColorOfBtnXacNhan = "#80bdfe";
            MuaBanString = "Mua";
            //StringOfXacNhanBtn = "Xác nhận mua";
            LbTienOrCK = "S.dư tiền";
            LbLoaiGD = "Thường";
        }
        public void SearchIdRealTime(string realTimeTextID)
        {
            if (!string.IsNullOrWhiteSpace(realTimeTextID) && !string.IsNullOrEmpty(realTimeTextID) && realTimeTextID != "")
            {
                if (GetCompany(realTimeTextID) != null)
                {
                    Company = new MaCompanyViewModel(GetCompany(realTimeTextID.ToUpper()));
                    IsEnable = IsEnableGia = true;
                }
            }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        public void CheckIDAfterUnfocus()
        {
            if (!string.IsNullOrWhiteSpace(TxtMa) && !string.IsNullOrEmpty(TxtMa))
            {
                if (GetCompany(TxtMa) == null)
                {
                    Company = new MaCompanyViewModel();
                    _pageService.DisplayAlert("Alert!", "Khong ton tai", "OK");
                    MessagingCenter.Send(this, "foucusID");
                    LoadData();
                }
            }
            else
                LoadData();
        }
        private async void OnSoDuCKClicked()
        {
            IsEnableSoDuCK = false;
            LbLoaiGD = "Thường";
            await _pageService.PushModelAsync(new SoDuCKPage());
            IsEnableSoDuCK = true;
        }       
        public void TxtPassWordChanged(string x)
        {
            CheckIsEnableXacNhan();
        }
        public void TxtKhoiLuongUnfocus()
        {
            if (TxtKhoiLuong != null && TxtKhoiLuong != "")
            {
                if (IsValidKL())
                {
                    int KL = Convert.ToInt32(TxtKhoiLuong);
                    if (MuaBanValue)// Trang thai Mua is true
                    {
                        if (TxtGia != null && TxtGia != "")
                        {
                            IsValidTaiSan();
                        }
                    }
                    else// trang thai BAN is true
                    {
                        if (KL > Company.KL)
                        {
                            _pageService.DisplayAlert("Alert!", "Ví chứng khoán không đủ", "OK");
                            FocusKL();
                        }
                    }
                }
            }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        public void TxtGiaUnfocus()
        {
            if (TxtGia != null)
            {
                if (IsValidGia())
                {
                    if (TxtKhoiLuong != null && IsValidKL())
                    {
                        IsValidTaiSan();
                    }
                }
            }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        public void TxtPassUnfocus()
        {
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void OnLoaiGDClicked()
        {
            if (LbLoaiGD.Equals("Thường"))
            {
                LbLoaiGD = "Margin";
                if (!MuaBanValue)
                {
                    IsEnableSoDuCK = false;
                    _pageService.PushModelAsync(new SoDuCKPage());
                    IsEnableSoDuCK = true;
                }
            }
            else
            { LbLoaiGD = "Thường"; }
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void OnCancelClick()
        {
            LoadData();
        }
        //btn gia tran, san, mua, ban, khop, TC
        private void OnFillGiaClicked(string parameter)
        {
            if (TxtMa != null && TxtMa!= "")
            {
                if (parameter.Equals("tran"))
                {
                    TxtGia = Company.PriceTran.ToString();
                }else if(parameter.Equals("tc"))
                {
                    TxtGia = Company.PriceTC.ToString();
                }
                else if (parameter.Equals("san"))
                {
                    TxtGia = Company.PriceSan.ToString();
                }
                else if (parameter.Equals("mua"))
                {
                    TxtGia = Company.PriceMua.ToString();
                }
                else if (parameter.Equals("khop"))
                {
                    TxtGia = Company.PriceKhop.ToString();
                }else
                {
                    TxtGia = Company.PriceBan.ToString();
                }
                IsEnableGia = true;
                //check KL
                if (TxtKhoiLuong != null)
                {
                    if (!IsValidTaiSan())
                    {
                        FocusGia();
                    }
                }
            }
            else
            {
                IsEnableGia = false;
            }                      
            CheckIsEnableXacNhan();
        }
        //
        private void OnStatusOfMuaBanClicked()
        {
            MuaBanValue = !MuaBanValue;
            if (!MuaBanValue && LbLoaiGD.Equals("Margin"))
            {
                _pageService.PushModelAsync(new SoDuCKPage());
            }
            TxtGia = TxtKhoiLuong = null;
            CheckMuaBan();
            CheckIsEnableXacNhan();
        }
        private void OnLbMaxClicked()
        {
            TxtKhoiLuong = max.ToString();
            if (TxtGia != null && TxtGia != "")
            {
                if (!IsValidTaiSan())
                {
                    FocusKL();
                }
            }
        }
        private void OnKhoiLuongClicked(string paramater)
        {
            if (paramater.Equals("0")) // minus
            {
                if (TxtKhoiLuong == "" || TxtKhoiLuong == null)
                {
                    TxtKhoiLuong = max.ToString();
                }
                else
                {
                    double KL = Convert.ToDouble(TxtKhoiLuong);
                    if (KL == 100)
                    {
                        _pageService.DisplayAlert("Alert!", "Khoi luong min= 100", "Ok");
                    }
                    else if (KL >= 200)
                    {
                        double newKL = KL - 100;
                        TxtKhoiLuong = newKL.ToString();
                    }
                }
            }
            else //plus khoi luong
            {
                if (TxtKhoiLuong == "" || TxtKhoiLuong == null)
                {
                    TxtKhoiLuong = "100";
                }
                else
                {
                    double KL = Convert.ToDouble(TxtKhoiLuong);
                    double newKL = KL + 100;
                    if (KL != max)
                    {
                        if (newKL > max)
                        {
                            _pageService.DisplayAlert("Alert!", "Khoi luong vuot muc", "Ok");
                            FocusKL();
                        }
                        else
                        {
                            TxtKhoiLuong = newKL.ToString();
                        }
                    }
                }
            }
            //check tai san
            if (TxtGia != null && TxtGia != "")
            {
                if (!IsValidTaiSan())
                {
                    FocusKL();
                }
            }
            CheckIsEnableXacNhan();
        }
        private void OnGiaClicked(string paramater)
        {
            if (paramater.Equals("0"))
            {
                if (TxtGia == "" || TxtGia == null)
                {
                    TxtGia = Company.PriceTran.ToString();
                }
                else if (Convert.ToDouble(TxtGia) > Company.PriceSan)
                {
                    double tempGia = Convert.ToDouble(TxtGia) - 0.1;
                    TxtGia = tempGia.ToString();
                }
            }
            else
            {
                if (TxtGia == "" || TxtGia == null)
                {
                    TxtGia = Company.PriceSan.ToString();
                }
                else if (Convert.ToDouble(TxtGia) < Company.PriceTran)
                {
                    double newGia = Convert.ToDouble(TxtGia) + 0.1;
                    TxtGia = newGia.ToString();
                }
            }
            //check
            if (TxtKhoiLuong != "" && TxtKhoiLuong != null)
            {
                if (IsValidGia())
                {
                    if (!IsValidTaiSan())
                    {
                        FocusGia();
                    }
                }
            }
            CheckIsEnableXacNhan();
        }
        // button loai Gia
        private void OnLoaiGiaClicked(string paramater)
        {
            if (paramater.Equals("LO"))
            {
                TxtGia = null;
                IsEnableGia = true;
            }
            else
            {
                IsEnableGia = false;
                if (paramater.Equals("ATC"))
                {
                    TxtGia = "ATC";
                }
                else if (paramater.Equals("ATO"))
                { TxtGia = "ATO"; }
                else
                { TxtGia = "MP"; }
            }
            CheckIsEnableXacNhan();
        }
        //
        public void CheckIsEnableXacNhan()
        {
            bool StatusOfXacNhan = default;
            if (string.IsNullOrWhiteSpace(TxtMa) || string.IsNullOrEmpty(TxtMa)
                || string.IsNullOrWhiteSpace(TxtGia) || string.IsNullOrEmpty(TxtGia)
                || string.IsNullOrWhiteSpace(TxtKhoiLuong) || string.IsNullOrEmpty(TxtKhoiLuong)
                || string.IsNullOrWhiteSpace(TxtPassWord) || string.IsNullOrEmpty(TxtPassWord))
            {
                StatusOfXacNhan = false;
            }
            else
            {
                StatusOfXacNhan = true;
            }
            //
            if (StatusOfXacNhan && MuaBanValue)
            {
                ColorOfBtnXacNhan = "#007aff";
            }
            else if (StatusOfXacNhan && !MuaBanValue)
            {
                ColorOfBtnXacNhan = "Red";
            }
            else if (!StatusOfXacNhan && MuaBanValue)
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
            bool result = true;
            if (TxtKhoiLuong.Equals(".") || TxtKhoiLuong.Equals("-"))
            {
                result = FocusKL();
            }
            else if (string.IsNullOrEmpty(TxtKhoiLuong) || string.IsNullOrWhiteSpace(TxtKhoiLuong))
            {
                result = FocusKL();
            }
            else
            {
                double temp = Convert.ToSingle(TxtKhoiLuong);
                if (temp < 100)
                {
                    _pageService.DisplayAlert("Alert!", "Khoi luong >=100", "OK");
                    result = FocusKL();
                }
                else if (temp % 100 != 0)
                {
                    _pageService.DisplayAlert("Alert!", "Khoi luong phai lai boi so cua 100", "OK");
                    result = FocusKL();
                }
                else if (temp > max)
                {
                    _pageService.DisplayAlert("Alert!", "Vuot Nguong", "OK");
                    result = FocusKL();
                }
            }
            return result;
        }
        private bool IsValidGia()
        {
            bool result = true;
            if (TxtGia.Equals(".") || TxtGia.Equals("-"))
            {
                result = FocusGia();
            }
            else if (string.IsNullOrEmpty(TxtGia) || string.IsNullOrWhiteSpace(TxtGia))
            {
                result = FocusGia();
            }
            else
            {
                double Gia = Convert.ToSingle(TxtGia);
                if (Gia < Company.PriceSan || Gia > Company.PriceTran)
                {
                    _pageService.DisplayAlert("Alert!", "Gia phải trong khoảng từ Sàn tới Trần", "ok");
                    result = FocusGia();
                }
            }
            return result;
        }
        private bool FocusKL()
        {
            MessagingCenter.Send(this, "foucusKhoiLuong");
            TxtKhoiLuong = null;
            return false;
        }
        private bool FocusGia()
        {
            MessagingCenter.Send(this, "foucusGia");
            TxtGia = null;
            return false;
        }
        private void CheckMuaBan()
        {
            if (MuaBanValue)
            {
                MuaBanString = "Mua";
                LbTienOrCK = "S.dư tiền";
                if (TxtMa != null)
                {
                    double tempMax = System.Math.Truncate(Convert.ToDouble(TongTaiSan / (Company.PriceSan * 1000)));
                    if (tempMax > Company.KL)
                    {
                        tempMax = Company.KL;
                    }
                    max = tempMax - tempMax % 100;
                }
                //color
                ColorOfBtnXacNhan = "#80bdfe";
                LbTongTaiSan = TongTaiSan.ToString();
            }
            else
            {
                MuaBanString = "Bán";
                LbTienOrCK = "Số dư CK";
                //color
                ColorOfBtnXacNhan = "#e18b8e";
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
            double KL = Convert.ToDouble(TxtKhoiLuong);
            if (!TxtGia.Equals("ATC") && !TxtGia.Equals("ATO") && !TxtGia.Equals("MP"))
            {
                double Gia = Convert.ToDouble(TxtGia);
                double temp = Gia * KL * 1000;
                if (temp > TongTaiSan)
                {
                    _pageService.DisplayAlert("Alert!", "Khong du tien", "OK");
                    result = false;
                }
            }
            return result;
        }
        //
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
        private ObservableCollection<MaCompanyViewModel> MaKhoiTao()
        {
            return Company.GetAllCompany();
        }
    }
}
