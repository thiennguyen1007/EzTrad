using EzTrad.Services;
using System;
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
        private FlyoutViewModel _selectedItem;
        //Command
        public ICommand LoadDataCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand FavoritesCommand { get; set; }
        public ICommand RemoveFavoritesCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand showLstQuanLyCommand { get; set; }
        public ICommand showLstBaoCaoGDCommmand { get; set; }
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
        public FlyoutViewModel SelectedItem { get => _selectedItem; set => SetProperty(ref _selectedItem, value); }

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
            //create list FlyoutItem
            LstFavorites = new ObservableCollection<FlyoutViewModel>();
            FlyoutItems = new ObservableCollection<FlyoutViewModel>(LstMenu());
            for (int i = 0; i < FlyoutItems.Count; i++)
            {
                FlyoutItems[i].IsTicked = "White";
            }
            //create list Flyout temp
            FlyoutItemsTemp = new ObservableCollection<FlyoutViewModel>(FlyoutItems);
        }
        public void OnSelectedItem(FlyoutViewModel x)
        {
            MessagingCenter.Send(this, "ChangeDetail", x);
            SelectedItem = null;
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
            //delete now, item in list Flyout          
            FlyoutItems.Remove(x);
            x.IsTicked = "#FFEB3B";
            LstFavorites.Add(x);
            CheckFavorIsAny();
            for (int i = 0; i < FlyoutItemsTemp.Count; i++)
            {
                if (x.LabelTitle.Equals(FlyoutItemsTemp[i].LabelTitle))
                {
                    FlyoutItemsTemp[i].IsTicked = "#FFEB3B";
                }
            }
        }
        private void OnRemoveFavorClicked(object obj)//remove item in favor list and change color of star
        {
            var x = obj as FlyoutViewModel;
            //remove item in list favorites
            if (x != null)
            {
                LstFavorites.Remove(x);
                x.IsTicked = "White";
                for (int i = 0; i < FlyoutItemsTemp.Count; i++)
                {
                    if (x.LabelTitle.Equals(FlyoutItemsTemp[i].LabelTitle))
                    {
                        FlyoutItemsTemp[i].IsTicked = "White";
                    }
                }
                FlyoutItems = new ObservableCollection<FlyoutViewModel>(FlyoutItemsTemp);
                CheckFavorIsAny();
                foreach (var item in LstFavorites)
                {
                    FlyoutItems.Remove(item);
                }
            }
        }
        private void OnShowQuanLyClicked()
        {
            IsShowDropDownQuanLyBtn = !IsShowDropDownQuanLyBtn;
            IsShowQuanLyBtn = !IsShowQuanLyBtn;
            CheckFavorIsAny();
            int duplicate = 0;
            if (IsShowFavor)
            {
                int numberLstFavor = LstFavorites.Count;
                int x = 45 * numberLstFavor;
                for (int i = 0; i < numberLstFavor; i++)
                {
                    if (LstFavorites[i].LabelTitle.Equals("Quản lý tài khoản"))
                    {
                        duplicate++;
                    }
                }
                if (duplicate != 0)
                {
                    x += ChangeHeightOfLstFavorites(IsShowDropDownBaoCaoGDBtn, IsShowDropDownQuanLyBtn);
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
            if (IsShowFavor)
            {
                int numberLstFavor = LstFavorites.Count;
                int x = 45 * numberLstFavor;
                for (int i = 0; i < numberLstFavor; i++)
                {
                    if (LstFavorites[i].LabelTitle.Equals("Báo cáo giao dịch"))
                    {
                        duplicate++;
                    }
                }
                if (duplicate != 0)
                {
                    x += ChangeHeightOfLstFavorites(IsShowDropDownBaoCaoGDBtn, IsShowDropDownQuanLyBtn);
                    duplicate = 0;
                }
                HeightOfLst = x.ToString();
            }
        }
        private int ChangeHeightOfLstFavorites(bool a, bool b)
        {
            if (a && !b)
            {
                return 90;
            }
            else if (a && b)
            {
                return 240;
            }
            else if (!a && b)
            {
                return 150;
            }
            else {
                return 0;
            }
        }
        private void CheckFavorIsAny()// check list favorites is any or not>>
        {
            if (LstFavorites == null)
            {
                IsShowFavor = false;
            }
            else if (LstFavorites.Count == 0)
            {
                IsShowFavor = false;
            }
            else
            {
                IsShowFavor = true;
                HeightLstFavor = 45 * LstFavorites.Count;
                HeightOfLst = HeightLstFavor.ToString();
            }
        }
        private void DeselectItem(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
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
                TargetPage = typeof(Views.DatLenhPage.DatLenhPage)
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
            FlyoutViewModel f13 = new FlyoutViewModel
            {
                LabelTitle = "Báo cáo tài sản",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f14 = new FlyoutViewModel
            {
                LabelTitle = "Bán lô lẻ",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f15 = new FlyoutViewModel
            {
                LabelTitle = "Ký quỹ",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f16 = new FlyoutViewModel
            {
                LabelTitle = "Thực hiện quyền",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f17 = new FlyoutViewModel
            {
                LabelTitle = "Tư vấn đầu tư",
                IsHeader = true
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f18 = new FlyoutViewModel
            {
                LabelTitle = "Giới thiệu dịch vụ",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f19 = new FlyoutViewModel
            {
                LabelTitle = "Tìm kiếm chuyên viên",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f20 = new FlyoutViewModel
            {
                LabelTitle = "Báo cáo tư vấn",
                //Icon = "mangAcc.png",
            };
            FlyoutViewModel f21 = new FlyoutViewModel
            {
                LabelTitle = "Đánh giá chuyên viên",
                //Icon = "mangAcc.png",
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
            lstTemp.Add(f13);
            lstTemp.Add(f14);
            lstTemp.Add(f15);
            lstTemp.Add(f16);
            lstTemp.Add(f17);
            lstTemp.Add(f18);
            lstTemp.Add(f19);
            lstTemp.Add(f20);
            lstTemp.Add(f21);
            return lstTemp;
        }
    }
}
