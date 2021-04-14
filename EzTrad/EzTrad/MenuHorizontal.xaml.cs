
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzTrad
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuHorizontal : TabbedPage
    {
        public MenuHorizontal(string x)
        {
            InitializeComponent();
            if (x == "Đặt lệnh")
            {
                CurrentPage = Children[1];
            }
            
        }
    }
}