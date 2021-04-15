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
            //flyoutPage.listView.ItemSelected += OnItemSelected;
            MessagingCenter.Subscribe<FlyoutMenuViewModel, FlyoutViewModel>(this, "ChangeDetail", OnItemSelected);
        }

        private void OnItemSelected(FlyoutMenuViewModel sender, FlyoutViewModel newDetailPage)
        {
            if (newDetailPage != null)
            {
                //Detail = new NavigationPage((Page)Activator.CreateInstance(newDetailPage.TargetPage));
                Detail = new NavigationPage(new MenuHorizontal(newDetailPage.LabelTitle));
                Detail.Title = newDetailPage.LabelTitle;
                IsPresented = false;
            }
        }
    }
}
