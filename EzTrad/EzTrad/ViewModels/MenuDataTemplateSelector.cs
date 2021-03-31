using EzTrad.ViewModels.FlyoutMenuViewModel;
using Xamarin.Forms;

namespace EzTrad.ViewModels
{
    public class MenuDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //return ((Flyout)item).IsHeader = true ? ValidTemplate : InvalidTemplate;
            if (((FlyoutViewModel)item).IsHeader == true)
            {
                return ValidTemplate;
            }
            else { return InvalidTemplate; }
            
        }
    }
}
