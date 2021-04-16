using EzTrad.ViewModels.FlyoutMenuViewModel;
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
            //unsub
            MessagingCenter.Unsubscribe<MainPage>(this, "ChangeDetail");
        }

        private void OnItemSelected(FlyoutMenuViewModel sender, FlyoutViewModel newDetailPage)
        {
            if (newDetailPage != null)
            {
                Detail = new NavigationPage(new MenuHorizontal(newDetailPage.LabelTitle));
                IsPresented = false;
            }
            //MessagingCenter.Unsubscribe<FlyoutMenuViewModel, FlyoutViewModel>(this, "ChangeDetail");
        }
    }
}
