using EzTrad.ViewModels.FlyoutMenuViewModel;
using Xamarin.Forms;

namespace EzTrad.ViewModels
{
    public class MenuDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }
        public DataTemplate BaoCaoGD{get; set;}
        public DataTemplate QuanlyTaiKhoan { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((FlyoutViewModel)item).IsHeader)
            {
                return ValidTemplate;
            }           
            else if (((FlyoutViewModel)item).LabelTitle.Equals("Báo cáo giao dịch"))
            {
                return BaoCaoGD;
            }
            else if (((FlyoutViewModel)item).LabelTitle.Equals("Quản lý tài khoản"))
            {
                return QuanlyTaiKhoan;
            }
            else { return InvalidTemplate; }
        }
    }
}
