using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzTrad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPage : ContentPage
    {
        public ObservableCollection<Models.Flyout> FlyoutItems { get; set; }
        public FlyoutMenuPage()
        {
            InitializeComponent();
            FlyoutItems = new ObservableCollection<Models.Flyout>()
            {
                new Models.Flyout{ Title="Thị trường",
                    IsHeader=true
                },
                new Models.Flyout{ Title="Tổng quan",
                    //TargetPage=typeof(Views.TongQuanPage),
                    Icon="tongQuan.png",
                },
                new Models.Flyout{ Title="Bảng giá",
                    //TargetPage=typeof(Views.About),
                    Icon="BangGia.png"
                },
                new Models.Flyout{ Title="Tin tức",
                    //TargetPage=typeof(Views.About),
                    Icon="news.png"
                },
                new Models.Flyout{ Title="Chỉ số thế giới",
                    //TargetPage=typeof(Views.About),
                    Icon="index.png"
                },
                new Models.Flyout{ Title="FPTS nhận định",
                    //TargetPage=typeof(Views.About),
                    Icon="nhanDinh.png"
                },
                new Models.Flyout{ Title="Lịch sự kiện",
                    //TargetPage=typeof(Views.About),
                    Icon="calendar.png"
                },
                new Models.Flyout{ Title="Biểu đồ",
                    //TargetPage=typeof(Views.About),
                    Icon="chart.png"
                },
                new Models.Flyout{ Title="Giao dịch phái sinh",
                    //TargetPage=typeof(Views.About),
                    Icon="transaction.png"
                },
            };
            listView.ItemsSource = FlyoutItems;
        }
    }
}