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
            InitializeComponent();
            if (txtName != null)
            {
                AnimationOfCompanyName();
            }
            //
            MessagingCenter.Subscribe<DatLenhPageViewModel>(this, "foucusID", FocusID);
            MessagingCenter.Subscribe<DatLenhPageViewModel>(this, "foucusKhoiLuong", FocusKhoiLuong);
            MessagingCenter.Subscribe<DatLenhPageViewModel>(this, "foucusGia", FocusGia);
            //unSub
            MessagingCenter.Unsubscribe<DatLenhPage>(this, "foucusID");
            MessagingCenter.Unsubscribe<DatLenhPage>(this, "foucusKhoiLuong");
            MessagingCenter.Unsubscribe<DatLenhPage>(this, "foucusGia");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var pageService = new PageService();
            ViewModel = new DatLenhPageViewModel(pageService);
            ViewModel.LoadDataCommand.Execute(null);
            btnXacNhan.TextColor = Color.White;
        }
        private async void AnimationOfCompanyName()
        {
            await Task.Delay(4000);
            while (true)
            {
                await txtName.TranslateTo(0, 0, 800);
                await txtName.TranslateTo(-300, 0, 7000);    // Move image left
                await txtName.TranslateTo(0, 0, 800);
                await txtName.TranslateTo(300, 0, 7000);     // Move image right
                await txtName.TranslateTo(0, 0, 800);
            }
        }
        public DatLenhPageViewModel ViewModel
        {
            get { return BindingContext as DatLenhPageViewModel; }
            set { BindingContext = value; }
        }
        private void txt_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchIdRealTime(e.NewTextValue);
        }
        private void btn_Tapped(object sender, EventArgs e)
        {
            var x = sender as StackLayout;
            FadeViewClicked(x);
            FadeGridClicked(GridGia);
        }
        private void PlusMinus_Tapped(object sender, EventArgs e)
        {
            var x = sender as Grid;
            FadeGridClicked(x);
        }
        private void btnPlusGia_Clicked(object sender, EventArgs e)
        {
            FadeGridClicked(PlusGia);
        }
        private void btnMinusGia_Clicked(object sender, EventArgs e)
        {
            FadeGridClicked(minusGia);
        }
        private void btnMinusKL_Clicked(object sender, EventArgs e)
        {
            FadeGridClicked(minusKL);
        }
        private void btnPlusKL_Clicked(object sender, EventArgs e)
        {
            FadeGridClicked(plusKL);
        }
        private void txtPassWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.TxtPassWordChanged(e.NewTextValue);
        }
        private void btnBackLoaiGD_Clicked(object sender, EventArgs e)
        {
            FadeGridClickedWhite(backLoaiGD);
        }
        private void btnNextLoaiGD_Clicked(object sender, EventArgs e)
        {
            FadeGridClickedWhite(nextLoaiGD);
        }
        private void btnBackMuaBan_Clicked(object sender, EventArgs e)
        {
            FadeGridClickedWhite(backMuaBan);
        }

        private void btnNextMuaBan_Clicked(object sender, EventArgs e)
        {
            FadeGridClickedWhite(nextMuaBan);
        }
        private async void FadeViewClicked(StackLayout x)
        {
            await Task.Delay(20);
            await x.FadeTo(0.5, 100);
            x.BackgroundColor = Color.FromHex("#c9c9c9");
            await Task.Delay(20);
            await x.FadeTo(1, 100);
            x.BackgroundColor = Color.FromHex("#4d4d4d");
        }
        private async void FadeGridClicked(Grid g)
        {
            await Task.Delay(20);
            await g.FadeTo(0.5, 100);
            g.BackgroundColor = Color.FromHex("#c9c9c9");
            await Task.Delay(20);
            await g.FadeTo(1, 100);
            g.BackgroundColor = Color.White;
        }
        private async void FadeGridClickedWhite(Grid g)
        {
            await Task.Delay(20);
            await g.FadeTo(0.5, 100);
            g.BackgroundColor = Color.White;
            await Task.Delay(20);
            await g.FadeTo(1, 100);
            g.BackgroundColor = Color.FromHex("#c9c9c9");
        }
        private void NextBack_Tapped(object sender, EventArgs e)
        {
            var x = sender as Grid;
            FadeGridClickedWhite(x);
        }
        private void lbKhoiLuong_Focused(object sender, FocusEventArgs e)
        {
            ViewModel.StatusOfXacNhan = false;
        }
        private void lbKhoiLuong_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.TxtKhoiLuongUnfocus();
        }
        private void lbGia_Focused(object sender, FocusEventArgs e)
        {
            ViewModel.StatusOfXacNhan = false;
        }
        private void lbGia_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.TxtGiaUnfocus();
        }
        private void FocusGia(DatLenhPageViewModel obj)
        {
            lbGia.Focus();
        }
        private void FocusKhoiLuong(object arg1)
        {
            lbKhoiLuong.Focus();
        }
        private void FocusID(DatLenhPageViewModel obj)
        {
            txtMa.Focus();
        }
        private void txtMa_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.CheckIDAfterUnfocus();
        }
        private void lbKLMax_Tapped(object sender, EventArgs e)
        {
            FadeGridClicked(GridKL);
        }
    }
}