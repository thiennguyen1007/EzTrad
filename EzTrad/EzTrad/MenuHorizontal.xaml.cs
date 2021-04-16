
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
            SetupPage(x); 
            CurrentPageChanged += (s, e) => Title = (CurrentPage as Page).Title ?? CurrentPage.Title;
        }
        private void SetupPage(string newTitle)
        {
            if (newTitle != null)
            {
                if (newTitle == "Đặt lệnh")
                {
                    CurrentPage = Children[1];

                }
                else if (newTitle == "Tổng quan")
                {
                    CurrentPage = Children[0];
                }
                Title = newTitle;
            }
        }
    }
}