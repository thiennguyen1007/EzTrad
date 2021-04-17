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
        }
        private void OnItemSelected(FlyoutMenuViewModel sender, FlyoutViewModel x)
        {
            if (x != null)
            {
                Detail = new NavigationPage(new MenuHorizontal(x.LabelTitle));
                MessagingCenter.Unsubscribe<MainPage>(this, "ChangeDetail");
            }
            IsPresented = false;            
        }
    }
}
