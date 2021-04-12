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
    public partial class SoDuCKPage : ContentPage
    {
        public SoDuCKPage()
        {
            InitializeComponent();
            ViewModel = new SoDuCKPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
        public SoDuCKPageViewModel ViewModel
        {
            get { return BindingContext as SoDuCKPageViewModel; }
            set { BindingContext = value; }
        }
    }
}