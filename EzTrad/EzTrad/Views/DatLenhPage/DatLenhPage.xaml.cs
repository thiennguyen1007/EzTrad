using EzTrad.ViewModels.DatLenhViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ViewModel = new DatLenhPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
            
            Task.Delay(2000);
            Task.Run(async ()=>
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
    }
}