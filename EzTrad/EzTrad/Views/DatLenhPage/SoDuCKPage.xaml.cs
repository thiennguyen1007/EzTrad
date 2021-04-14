using EzTrad.Services;
using EzTrad.ViewModels;
using EzTrad.ViewModels.DatLenhViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace EzTrad.Views.DatLenhPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoDuCKPage : PopupPage
    {
        public SoDuCKPage()
        {
            var pageService = new PageService();
            InitializeComponent();
            ViewModel = new SoDuCKPageViewModel(pageService);
        }
        public SoDuCKPageViewModel ViewModel
        {
            get { return BindingContext as SoDuCKPageViewModel; }
            set { BindingContext = value; }
        }

        private void CustomEntry_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            return;
        }

        private void CustomEntry_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ViewModel.SearchCompany();
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var x = e.SelectedItem as MaCompanyViewModel;
            ViewModel.SoDuCKSelected(x);
        }
    }
}