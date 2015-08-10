using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PopupTranslator.Annotations;

namespace PopupTranslator.ViewModel
{
    public sealed class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly IHotkeyService hotkeyService;
        private readonly ITranslator translator;
        private Key actionKeyPressed;
        private ModifierKeys modifierKeysPressed;
        private Language selectedSourceLanguage;
        private Language selectedTargetLanguage;

        public SettingsViewModel(IHotkeyService hotkeyService, ITranslator translator)
        {
            this.hotkeyService = hotkeyService;
            this.translator = translator;

            SupportedLanguages = new ObservableCollection<Language>(translator.Languages);
            SelectedSourceLanguage = translator.SourceLanguage;
            SelectedTargetLanguage = translator.TargetLanguage;
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

        public Language SelectedSourceLanguage
        {
            get { return selectedSourceLanguage; }
            set
            {
                selectedSourceLanguage = value;
                OnPropertyChanged();
            }
        }

        public Language SelectedTargetLanguage
        {
            get { return selectedTargetLanguage; }
            set
            {
                selectedTargetLanguage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Language> SupportedLanguages { get; set; }

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
            translator.SourceLanguage = selectedSourceLanguage;
            translator.TargetLanguage = selectedTargetLanguage;

            OnPropertyChanged(nameof(CurrentSetHotkeys));
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}