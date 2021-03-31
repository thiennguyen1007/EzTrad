using System;

namespace EzTrad.ViewModels.FlyoutMenuViewModel
{
    public class FlyoutViewModel : BaseViewModel
    {
        private string _title;
        private Type _targetPage;
        private string _icon;
        private bool _isHeader;
        private string _isTicked;

        public string Title1
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public Type TargetPage { get => _targetPage;
            set => SetProperty(ref _targetPage, value); }
        public string Icon { get => _icon;
            set => SetProperty(ref _icon, value);
        }
        public bool IsHeader { get => _isHeader;
            set => SetProperty(ref _isHeader, value); }
        public string IsTicked { get => _isTicked; set => SetProperty(ref _isTicked, value); }
    }
}
