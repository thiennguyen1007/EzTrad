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
            Title = x;
            CurrentPageChanged += (s, e) => Title = (CurrentPage as Page).Title ?? CurrentPage.Title;
        }
        private void SetupPage(string newTitle)
        {
            if (newTitle != null)
            {
                if (newTitle.Equals("Đặt lệnh"))
                {
                    CurrentPage = Children[2];
                }
                else if (newTitle.Equals("Tổng quan"))
                {
                    CurrentPage = Children[0];
                }
            }
        }
    }
}