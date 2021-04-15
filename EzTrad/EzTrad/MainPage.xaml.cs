using EzTrad.ViewModels.FlyoutMenuViewModel;
using System;
using Xamarin.Forms;

namespace EzTrad
{
    public partial class MainPage : FlyoutPage
    {      
        public MainPage()
        {
            InitializeComponent();
            Detail.Title = "Tổng quan";
            MessagingCenter.Subscribe<FlyoutMenuViewModel, FlyoutViewModel>(this, "ChangeDetail", OnItemSelected);
        }

        private void OnItemSelected(FlyoutMenuViewModel sender, FlyoutViewModel newDetailPage)
        {
            if (newDetailPage != null)
            {
                Detail = new NavigationPage(new MenuHorizontal(newDetailPage.LabelTitle));
                IsPresented = false;
            }
        }
    }
}
