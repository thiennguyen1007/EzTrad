using EzTrad.Services;
using EzTrad.ViewModels.FlyoutMenuViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzTrad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPage : ContentPage
    {
        public FlyoutMenuPage()
        {
            var pageService = new PageService();
            ViewModel = new FlyoutMenuViewModel(pageService);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
        private FlyoutMenuViewModel ViewModel
        {
            get => BindingContext as FlyoutMenuViewModel;
            set { BindingContext = value; }
        }
        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.FavoritesCommand.Execute(null);
        }
        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.OnSelectedItem(e.SelectedItem as FlyoutViewModel);
        }
    }
}