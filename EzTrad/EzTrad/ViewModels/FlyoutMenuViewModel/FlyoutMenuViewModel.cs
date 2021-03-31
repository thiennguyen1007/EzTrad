using EzTrad.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.FlyoutMenuViewModel
{
    public class FlyoutMenuViewModel : BaseViewModel
    {
        private IPageService _pageService;
        private ObservableCollection<FlyoutViewModel> _flyoutItems;
        private int temp { get; set; } = 0;
        private bool _starTick;

        //Command
        public ICommand LoadDataCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand FavoritesCommand { get; set; }
        //
        public ObservableCollection<FlyoutViewModel> FlyoutItems
        {
            get => _flyoutItems;
            set { SetProperty(ref _flyoutItems, value); }
        }
        public bool StarTick
        {
            get => _starTick;
            set { SetProperty(ref _starTick, value); }
        }
        public FlyoutMenuViewModel() { }
        public FlyoutMenuViewModel(IPageService pageService)
        {
            _pageService = pageService;
            //Command
            LoadDataCommand = new Command(LoadData);
            LogoutCommand = new Command(OnLogoutClicked);
            FavoritesCommand = new Command(OnFavoritesClicked);
            SettingCommand = new Command(OnSettingClicked);
        }
        private void LoadData()
        {
            FlyoutItems = new ObservableCollection<FlyoutViewModel>(LstMenu());
            for (int i = 0; i < FlyoutItems.Count; i++)
            {
                FlyoutItems[i].IsTicked = "White";
            }
            StarTick = IsBusy;
        }
        private void OnLogoutClicked()
        {
            _pageService.DisplayAlert("Alert!", "Log out now?", "Yes", "No");
        }
        private void OnSettingClicked()
        {
            if (IsBusy == false)
            {
                IsBusy = true;
                StarTick = IsBusy;
            }
            else
            {
                IsBusy = false;
                StarTick = IsBusy;
            }
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
                x.IsTicked = "white";
            }
            for (int i = 0; i < FlyoutItems.Count; i++)
            {
                if (FlyoutItems[i].Title == x.Title)
                {
                    FlyoutItems[i].IsTicked = x.IsTicked;
                }
            }
        }
        private ObservableCollection<FlyoutViewModel> LstMenu()
        {
            ObservableCollection<FlyoutViewModel> lstTemp = new ObservableCollection<FlyoutViewModel>();
            FlyoutViewModel f = new FlyoutViewModel
            {
                Title = "Thị trường",
                IsHeader = true
            };
            FlyoutViewModel f1 = new FlyoutViewModel
            {
                Title = "Tổng quan",
                Icon = "tongQuan.png",
            };
            FlyoutViewModel f2 = new FlyoutViewModel
            {
                Title = "Bảng giá",
                Icon = "BangGia.png",
            };
            FlyoutViewModel f3 = new FlyoutViewModel
            {
                Title = "Tin tức",
                Icon = "news.png",
            };
            FlyoutViewModel f4 = new FlyoutViewModel
            {
                Title = "Chỉ số thế giới",
                Icon = "index.png",
            };
            FlyoutViewModel f5 = new FlyoutViewModel
            {
                Title = "FPTS nhận định",
                Icon = "nhanDinh.png",
            };
            FlyoutViewModel f6 = new FlyoutViewModel
            {
                Title = "Lịch sự kiện",
                Icon = "calendar.png",
            };
            FlyoutViewModel f7 = new FlyoutViewModel
            {
                Title = "Biểu đồ",
                Icon = "chart.png",
            };
            FlyoutViewModel f8 = new FlyoutViewModel
            {
                Title = "Giao dịch phái sinh",
                Icon = "transaction.png",
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
            return lstTemp;
        }
    }
}
