using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PopupTranslator.Annotations;

namespace PopupTranslator.ViewModel
{
    public sealed class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly IHotkeyService hotkeyService;
        private Key actionKeyPressed;
        private ModifierKeys modifierKeysPressed;

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

        public string CurrentSetHotkeys => $"{hotkeyService.ModifierKeys} + {hotkeyService.ActionKey}";

        public ICommand ApplySettingsAndCloseCommand => new RelayCommand(ApplySettingsAndClose);
        public ICommand ApplySettingsCommand => new RelayCommand(ApplySettings);
        public ICommand CloseCommand => new RelayCommand(CloseSettings);

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler RequestedClose;

        private void ApplySettingsAndClose(object obj)
        {
            ApplySettings(null);

            CloseSettings(null);
        }

        private void CloseSettings(object obj)
        {
            RequestedClose?.Invoke(this, EventArgs.Empty);
        }

        private void ApplySettings(object obj)
        {
            hotkeyService.SetNewHotkeys(ActionKeyPressed, ModifierKeysPressed);
            OnPropertyChanged(nameof(CurrentSetHotkeys));
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}