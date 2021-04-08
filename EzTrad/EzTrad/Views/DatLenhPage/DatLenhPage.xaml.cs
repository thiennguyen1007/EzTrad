using EzTrad.Services;
using EzTrad.ViewModels.DatLenhViewModel;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzTrad.Views.DatLenhPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatLenhPage : ContentPage
    {
        public DatLenhPage()
        {
            var pageService = new PageService();
            InitializeComponent();
            ViewModel = new DatLenhPageViewModel(pageService);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);

            Task.Delay(2000);
            Task.Run(async () =>
                {
                    while (true)
                    {
                        await txtName.TranslateTo(0, 0, 1000);
                        await txtName.TranslateTo(-300, 0, 7000);    // Move image left
                        await txtName.TranslateTo(0, 0, 1000);
                        await txtName.TranslateTo(300, 0, 7000);     // Move image right
                        await txtName.TranslateTo(0, 0, 1000);
                    }
                }
            );
        }
        public DatLenhPageViewModel ViewModel
        {
            get { return BindingContext as DatLenhPageViewModel; }
            set { BindingContext = value; }
        }
        private void txt_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchChanged(e.NewTextValue);
        }
        private async void btn_Tapped(object sender, EventArgs e)
        {
            var x = sender as StackLayout;
            FadeViewClicked(x);
            await Task.Delay(50);
            await BoxGia.FadeTo(0.5, 100);
            BoxGia.BackgroundColor = Color.FromHex("#c9c9c9");
            await Task.Delay(50);
            await BoxGia.FadeTo(1, 100);
            BoxGia.BackgroundColor = Color.White;
        }
        private async void FadeViewClicked(StackLayout x)
        {
            await Task.Delay(50);
            await x.FadeTo(0.5, 100);
            await Task.Delay(50);
            await x.FadeTo(1, 100);
        }

        private void TxtKhoiLuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.TxtKhoiLuongChangedCheck(e.NewTextValue);
        }
    }
}