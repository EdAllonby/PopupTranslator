using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PopupTranslator.Annotations;

namespace PopupTranslator
{
    public sealed class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly IHotkeyService hotkeyService;
        private Key actionKeyPressed;
        private ModifierKeys modifierKeysPressed;

        public event EventHandler RequestedClose;

        public SettingsViewModel(IHotkeyService hotkeyService)
        {
            this.hotkeyService = hotkeyService;
        }

        public ModifierKeys ModifierKeysPressed
        {
            get { return modifierKeysPressed; }
            set
            {
                modifierKeysPressed = value;
                OnPropertyChanged();
            }
        }

        public Key ActionKeyPressed
        {
            get { return actionKeyPressed; }
            set
            {
                actionKeyPressed = value;
                OnPropertyChanged();
            }
        }

        public ICommand ApplySettingsCommand => new RelayCommand(ApplySettings);

        public event PropertyChangedEventHandler PropertyChanged;

        private void ApplySettings(object obj)
        {
            hotkeyService.SetNewHotkeys(ActionKeyPressed, ModifierKeysPressed);

            RequestedClose?.Invoke(this, EventArgs.Empty);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}