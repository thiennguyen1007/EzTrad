using EzTrad.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.FlyoutMenuViewModel
{
    public class FlyoutMenuViewModel : BaseViewModel
    {
        private IPageService _pageService;
        //list
        private ObservableCollection<FlyoutViewModel> _flyoutItems;
        private static ObservableCollection<FlyoutViewModel> _lstFavorites;
        private static ObservableCollection<FlyoutViewModel> _favoritesTemp = new ObservableCollection<FlyoutViewModel>();
        private ObservableCollection<FlyoutViewModel> _flyoutItemsTemp;
        private int temp { get; set; } = 0;
        private int _heightLstFavor;
        private bool _starTick = false;
        private bool _isShowFavor;
        private bool _isShowPowerBtn;
        private bool _isShowUpdateBtn;
        private string _heightOfLst;
        private bool _isShowBaoCaoGDBtn;
        private bool _isShowBtnQuanLyBtn;
        private bool _isShowDropDownBaoCaoBtn;
        private bool _isShowDropDownQuanLyBtn;
        //Command
        public ICommand LoadDataCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand FavoritesCommand { get; set; }
        public ICommand RemoveFavoritesCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand showLstQuanLyCommand { get; set; }
        public ICommand showLstBaoCaoGDCommmand { get; set; }
        //
        public ObservableCollection<FlyoutViewModel> FlyoutItems
        {
            get => _flyoutItems;
            set { SetProperty(ref _flyoutItems, value); }
        }
        public ObservableCollection<FlyoutViewModel> LstFavorites
        {
            get => _lstFavorites;
            set { SetProperty(ref _lstFavorites, value); }
        }
        public ObservableCollection<FlyoutViewModel> FavoritesTemp
        {
            get => _favoritesTemp;
            set { SetProperty(ref _favoritesTemp, value); }
        }
        public ObservableCollection<FlyoutViewModel> FlyoutItemsTemp
        {
            get => _flyoutItemsTemp;
            set { SetProperty(ref _flyoutItemsTemp, value); }
        }
        public bool StarTick
        {
            get => _starTick;
            set { SetProperty(ref _starTick, value); }
        }
        public bool IsShowFavor
        {
            get => _isShowFavor;
            set { SetProperty(ref _isShowFavor, value); }
        }
        public bool IsShowPowerBtn
        {
            get => _isShowPowerBtn;
            set { SetProperty(ref _isShowPowerBtn, value); }
        }
        public bool IsShowUpdateBtn
        {
            get => _isShowUpdateBtn;
            set { SetProperty(ref _isShowUpdateBtn, value); }
        }
        public string HeightOfLst
        {
            get => _heightOfLst;
            set { SetProperty(ref _heightOfLst, value); }
        }
        public bool IsShowBaoCaoGDBtn
        {
            get => _isShowBaoCaoGDBtn;
            set { SetProperty(ref _isShowBaoCaoGDBtn, value); }
        }
        public bool IsShowQuanLyBtn
        {
            get => _isShowBtnQuanLyBtn;
            set { SetProperty(ref _isShowBtnQuanLyBtn, value); }
        }
        public bool IsShowDropDownQuanLyBtn
        {
            get => _isShowDropDownQuanLyBtn;
            set { SetProperty(ref _isShowDropDownQuanLyBtn, value); }
        }
        public bool IsShowDropDownBaoCaoGDBtn
        {
            get => _isShowDropDownBaoCaoBtn;
            set => SetProperty(ref _isShowDropDownBaoCaoBtn, value);
        }
        public int HeightLstFavor
        {
            set => SetProperty(ref _heightLstFavor, value);
            get => _heightLstFavor;
        }
        //implement ...
        public FlyoutMenuViewModel(IPageService pageService)
        {
            _pageService = pageService;
            //Command
            LoadDataCommand = new Command(LoadData);
            LogoutCommand = new Command(OnLogoutClicked);
            FavoritesCommand = new Command(OnFavoritesClicked);
            SettingCommand = new Command(OnSettingClicked);
            UpdateCommand = new Command(OnUpdateClicked);
            RemoveFavoritesCommand = new Command(OnRemoveFavorClicked);
            showLstBaoCaoGDCommmand = new Command(OnShowBaoCaoGDClicked);
            showLstQuanLyCommand = new Command(OnShowQuanLyClicked);
            //
        }
        private void LoadData()
        {
            //View setup
            StarTick = IsBusy;
            IsShowPowerBtn = true;
            IsShowUpdateBtn = false;
            IsShowBaoCaoGDBtn = true;
            IsShowQuanLyBtn = true;
            IsShowDropDownBaoCaoGDBtn = false;
            IsShowDropDownQuanLyBtn = false;
            //
            FlyoutItems = new ObservableCollection<FlyoutViewModel>(LstMenu());
            for (int i = 0; i < FlyoutItems.Count; i++)
            {
                FlyoutItems[i].IsTicked = "White";
            }
            FlyoutItemsTemp = new ObservableCollection<FlyoutViewModel>(FlyoutItems);
        }
        private void OnLogoutClicked()
        {
            _pageService.DisplayAlert("Alert!", "Log out now?", "Yes", "No");
        }
        private void OnSettingClicked()
        {
            //view
            IsShowPowerBtn = false;
            IsShowUpdateBtn = true;
            IsShowBaoCaoGDBtn = false;
            IsShowQuanLyBtn = false;
            IsShowDropDownBaoCaoGDBtn = false;
            IsShowDropDownQuanLyBtn = false;
            StarTick = true;
            //
            FlyoutItems = new ObservableCollection<FlyoutViewModel>(FlyoutItemsTemp);
        }
        private void OnUpdateClicked()
        {
            //View
            IsShowUpdateBtn = false;
            IsShowPowerBtn = true;
            IsShowBaoCaoGDBtn = true;
            IsShowQuanLyBtn = true;
            IsShowDropDownBaoCaoGDBtn = false;
            IsShowDropDownQuanLyBtn = false;
            StarTick = false;
            //
            CheckFavorIsAny();
            if (LstFavorites != null)
            {
                int duplicateNumber = 0;
                for (int i = 0; i < LstFavorites.Count; i++)
                {
                    for (int j = 0; j < FlyoutItemsTemp.Count; j++)
                    {
                        if (LstFavorites[i].LabelTitle == FlyoutItemsTemp[j].LabelTitle)
                        {
                            duplicateNumber++;
                        }
                    }
                    if (duplicateNumber > 0)
                    {
                        FlyoutItems.Remove(LstFavorites[i]);
                        //reset
                        duplicateNumber = 0;
                    }
                }
            }
            else
            {
                FlyoutItems = new ObservableCollection<FlyoutViewModel>(FlyoutItemsTemp);
            }
            LstFavorites = new ObservableCollection<FlyoutViewModel>(FavoritesTemp);
        }
        private void OnFavoritesClicked(object obj)
        {
            var x = obj as FlyoutViewModel;
            if (x.IsTicked == "White")
            {
                x.IsTicked = "#FFEB3B";
            }
            else
            {
                x.IsTicked = "White";
            }
            for (int i = 0; i < FlyoutItemsTemp.Count; i++)
            {
                if (FlyoutItemsTemp[i].Title == x.LabelTitle)
                {
                    FlyoutItemsTemp[i].IsTicked = x.IsTicked;
                }
            }
            //if in list favorites have duplicate this item => remove it else {add new item}
            CheckFavorIsAny();
            if (IsShowFavor == true)
            {
                for (int i = 0; i < LstFavorites.Count; i++)
                {
                    if (x.LabelTitle.Equals(LstFavorites[i].LabelTitle))// true when duplicate => remove it
                    {
                        FavoritesTemp.Remove(x);
                        temp++;
                    }
                }
                if (temp == 0)// not duplicate => add 
                {
                    FavoritesTemp.Clear();// reset list favor to setup 
                    for (int i = 0; i < FlyoutItems.Count; i++)
                    {
                        if (FlyoutItems[i].IsTicked.Equals("#FFEB3B"))//check what item is ticked to add
                        {
                            FavoritesTemp.Add(FlyoutItems[i]);
                        }
                    }
                }
            }
            else// list favor is (/0)
            {
                //add to list favorites          
                for (int i = 0; i < FlyoutItems.Count; i++)
                {
                    if (FlyoutItems[i].IsTicked.Equals("#FFEB3B"))//check what item is ticked to add
                    {
                        FavoritesTemp.Add(FlyoutItems[i]);
                    }
                }
            }
            CheckFavorIsAny();
            //reset temp
            temp = 0;
        }
        private void OnRemoveFavorClicked(object obj)//remove item in favor list and change color of star
        {
            var x = obj as FlyoutViewModel;
            int duplicate = 0;
            //remove item in list favorites
            if (x != null)
            {
                FavoritesTemp.Remove(x);
                LstFavorites = new ObservableCollection<FlyoutViewModel>(FavoritesTemp);
                //show change in view
                for (int i = 0; i < FlyoutItems.Count; i++)
                {
                    if (x.LabelTitle == FlyoutItems[i].LabelTitle)
                    {
                        duplicate++;
                    }
                }
                if (duplicate != 0)
                {
                    duplicate = 0;
                }
                else//not duplicate => add item to Flyout list
                {
                    //check color
                    FlyoutItems = new ObservableCollection<FlyoutViewModel>(FlyoutItemsTemp);
                    for (int i = 0; i < FlyoutItems.Count; i++)
                    {
                        if (x.LabelTitle == FlyoutItems[i].LabelTitle)
                        {
                            FlyoutItems[i].IsTicked = "White";
                        }
                    }
                    //change list flyout item

                    CheckFavorIsAny();
                }
                // check color of star in item
                for (int i = 0; i < FlyoutItemsTemp.Count; i++)
                {
                    if (x.LabelTitle == FlyoutItemsTemp[i].LabelTitle)
                    {
                        FlyoutItemsTemp[i].IsTicked = "White";
                    }
                }
                CheckFavorIsAny();
            }
            else
            {
                return;
            }
        }
        private void OnShowQuanLyClicked()
        {
            IsShowDropDownQuanLyBtn = !IsShowDropDownQuanLyBtn;
            IsShowQuanLyBtn = !IsShowQuanLyBtn;
            CheckFavorIsAny();
            int duplicate = 0;
            
            if (IsShowFavor == true)
            {
                int x = 45 * LstFavorites.Count;
                for (int i = 0; i < LstFavorites.Count; i++)
                {
                    if (LstFavorites[i].LabelTitle.Equals("Quản lý tài khoản"))
                    {
                        duplicate++;
                    }
                }
                if (duplicate == 0)
                {
                    return;
                }
                else
                {
                    if (IsShowDropDownBaoCaoGDBtn == true && IsShowDropDownQuanLyBtn == false)
                    {
                        x += 90;
                        //HeightLstFavor = x;
                    }
                    else if (IsShowDropDownBaoCaoGDBtn == true && IsShowDropDownQuanLyBtn == true)
                    {
                        x += 240;
                    }
                    else if (IsShowDropDownBaoCaoGDBtn == false && IsShowDropDownQuanLyBtn == true)
                    {
                        x += 150;
                    }
                    else
                    {
                        x = LstFavorites.Count * 45;
                    }
                    //reset
                    duplicate = 0;
                }
                HeightOfLst = x.ToString();
            }          
        }
        private void OnShowBaoCaoGDClicked()
        {
            IsShowDropDownBaoCaoGDBtn = !IsShowDropDownBaoCaoGDBtn;
            IsShowBaoCaoGDBtn = !IsShowBaoCaoGDBtn;
            CheckFavorIsAny();
            int duplicate = 0;
            if (IsShowFavor == true)
            {
                int x = 45 * LstFavorites.Count;
                for (int i = 0; i < LstFavorites.Count; i++)
                {
                    if (LstFavorites[i].LabelTitle.Equals("Báo cáo giao dịch"))
                    {
                        duplicate++;
                    }
                }
                if (duplicate == 0)
                {
                    return;
                }
                else
                {
                    if (IsShowDropDownBaoCaoGDBtn == true && IsShowDropDownQuanLyBtn == false)
                    {
                        x += 90;
                        //HeightLstFavor = x;
                    }
                    else if (IsShowDropDownBaoCaoGDBtn == true && IsShowDropDownQuanLyBtn == true)
                    {
                        x += 240;
                    }
                    else if (IsShowDropDownBaoCaoGDBtn == false && IsShowDropDownQuanLyBtn == true)
                    {
                        x += 150;
                    }
                    else
                    {
                        x = LstFavorites.Count * 45;
                    }
                    duplicate = 0;
                }
                HeightOfLst = x.ToString();
            }
        }
        private void CheckFavorIsAny()// check list favorites is any or not>>
        {
            if (FavoritesTemp == null)
            {
                IsShowFavor = false;
            }
            else if (FavoritesTemp.Count == 0)
            {
                IsShowFavor = false;
            }
            else
            {
                IsShowFavor = true;
                LstFavorites = new ObservableCollection<FlyoutViewModel>(FavoritesTemp);
                HeightLstFavor = 45 * LstFavorites.Count;
                HeightOfLst = HeightLstFavor.ToString();
            }
        }
        private ObservableCollection<FlyoutViewModel> LstMenu()
        {
            ObservableCollection<FlyoutViewModel> lstTemp = new ObservableCollection<FlyoutViewModel>();
            FlyoutViewModel f = new FlyoutViewModel
            {
                LabelTitle = "Thị trường",
                IsHeader = true
            };
            FlyoutViewModel f1 = new FlyoutViewModel
            {
                LabelTitle = "Tổng quan",
                Icon = "tongQuan.png",
            };
            FlyoutViewModel f2 = new FlyoutViewModel
            {
                LabelTitle = "Bảng giá",
                Icon = "BangGia.png",
            };
            FlyoutViewModel f3 = new FlyoutViewModel
            {
                LabelTitle = "Tin tức",
                Icon = "news.png",
            };
            FlyoutViewModel f4 = new FlyoutViewModel
            {
                LabelTitle = "Chỉ số thế giới",
                Icon = "index.png",
            };
            FlyoutViewModel f5 = new FlyoutViewModel
            {
                LabelTitle = "FPTS nhận định",
                Icon = "nhanDinh.png",
            };
            FlyoutViewModel f6 = new FlyoutViewModel
            {
                LabelTitle = "Lịch sự kiện",
                Icon = "calendar.png",
            };
            FlyoutViewModel f7 = new FlyoutViewModel
            {
                LabelTitle = "Biểu đồ",
                Icon = "chart.png",
            };
            FlyoutViewModel f8 = new FlyoutViewModel
            {
                LabelTitle = "Giao dịch phái sinh",
                Icon = "transaction.png",
            };
            FlyoutViewModel f9 = new FlyoutViewModel
            {
                IsHeader = true,
                LabelTitle = "Giao dịch"
            };
            FlyoutViewModel f10 = new FlyoutViewModel
            {
                LabelTitle = "Đặt lệnh",
                Icon = "paper.png",
            };
            FlyoutViewModel f11 = new FlyoutViewModel
            {
                LabelTitle = "Báo cáo giao dịch",
                Icon = "stockMarket.png"
            };
            FlyoutViewModel f12 = new FlyoutViewModel
            {
                LabelTitle = "Quản lý tài khoản",
                Icon = "mangAcc.png",
            };
            lstTemp.Add(f);
            lstTemp.Add(f1);
            lstTemp.Add(f2);
            lstTemp.Add(f3);
            lstTemp.Add(f4);
            lstTemp.Add(f5);
            lstTemp.Add(f6);
            lstTemp.Add(f7);
            lstTemp.Add(f8);
            lstTemp.Add(f9);
            lstTemp.Add(f10);
            lstTemp.Add(f11);
            lstTemp.Add(f12);
            return lstTemp;
        }
    }
}
