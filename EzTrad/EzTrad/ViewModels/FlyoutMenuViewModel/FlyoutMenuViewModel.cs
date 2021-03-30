using EzTrad.Models;
using System.Collections.ObjectModel;

namespace EzTrad.ViewModels.FlyoutMenuViewModel
{
    public class FlyoutMenuViewModel
    {
        private ObservableCollection<Flyout> LstMenu()
        {
            ObservableCollection<Flyout> lstTemp = new ObservableCollection<Flyout>();
            Flyout f = new Flyout
            {
                Title = "Thị trường",
                IsHeader = true
            };
            lstTemp.Add(f);
            return lstTemp;
        }
    }
}
