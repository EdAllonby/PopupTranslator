using System.ComponentModel;
using System.Runtime.CompilerServices;
using PopupTranslator.Annotations;
using PopupTranslator.Utility;

namespace PopupTranslator.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.SafeFireEvent(this, propertyName);
        }
    }
}