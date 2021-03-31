using EzTrad.Models;
using EzTrad.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzTrad.ViewModels.FlyoutMenuViewModel
{
    public class FlyoutMenuViewModel : BaseViewModel
    {
        private IPageService _pageService;
        private ObservableCollection<Flyout> _flyoutItems;
        public string StarColor 
        { 
            get => _starColor; 
            set => SetProperty(ref _starColor, value); 
        }
        private bool _starTick;
        private string _starColor = "Transparent";

        //Command
        public ICommand LoadDataCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand FavoritesCommand { get; set; }
        //
        public ObservableCollection<Flyout> FlyoutItems
        {
            get => _flyoutItems;
            set { SetProperty(ref _flyoutItems, value); }
        }
        public bool StarTick
        {
            get => _starTick;
            set { SetProperty(ref _starTick, value); }
        }
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
            FlyoutItems = new ObservableCollection<Flyout>(LstMenu());
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
        private void OnFavoritesClicked()
        {
            if (StarColor == "White")
            {
                StarColor = "#FFEB3B";
            }
            else
            {
                StarColor = "White";
            }          
        }
        private ObservableCollection<Flyout> LstMenu()
        {
            ObservableCollection<Flyout> lstTemp = new ObservableCollection<Flyout>();
            Flyout f = new Flyout
            {
                Title = "Thị trường",
                IsHeader = true
            };
            Flyout f1 = new Flyout
            {
                Title = "Tổng quan",
                Icon = "tongQuan.png",
            };
            Flyout f2 = new Flyout
            {
                Title = "Bảng giá",
                Icon = "BangGia.png",
            };
            Flyout f3 = new Flyout
            {
                Title = "Tin tức",
                Icon = "news.png",
            };
            Flyout f4 = new Flyout
            {
                Title = "Chỉ số thế giới",
                Icon = "index.png",
            };
            Flyout f5 = new Flyout
            {
                Title = "FPTS nhận định",
                Icon = "nhanDinh.png",
            };
            Flyout f6 = new Flyout
            {
                Title = "Lịch sự kiện",
                Icon = "calendar.png",
            };
            Flyout f7 = new Flyout
            {
                Title = "Biểu đồ",
                Icon = "chart.png",
            };
            Flyout f8 = new Flyout
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
