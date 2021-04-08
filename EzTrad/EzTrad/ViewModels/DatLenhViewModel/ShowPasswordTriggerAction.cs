using System.ComponentModel;
using Xamarin.Forms;

namespace EzTrad.ViewModels.DatLenhViewModel
{
    public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string ShowIcon { get; set; }
        public string HideIcon { get; set; }

        private bool _hidePassword = true;

        public bool HidePassword
        {
            set
            {
                if (_hidePassword != value)
                {
                    _hidePassword = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
                }
            }
            get => _hidePassword;
        }

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = HidePassword ? ShowIcon : HideIcon;
            HidePassword = !HidePassword;
        }
    }
}
