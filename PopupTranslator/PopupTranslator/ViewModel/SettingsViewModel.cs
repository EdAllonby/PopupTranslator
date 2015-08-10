using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PopupTranslator.Utility;

namespace PopupTranslator.ViewModel
{
    public sealed class SettingsViewModel : ViewModelBase
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

            ActionKeyPressed = hotkeyService.ActionKey;
            ModifierKeysPressed = hotkeyService.ModifierKeys;
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

        public event EventHandler RequestedClose;

        private void ApplySettingsAndClose(object obj)
        {
            ApplySettings(null);

            CloseSettings(null);
        }

        private void CloseSettings(object obj)
        {
            RequestedClose.SafeFireEvent(this);
        }

        private void ApplySettings(object obj)
        {
            hotkeyService.SetNewHotkeys(ActionKeyPressed, ModifierKeysPressed);
            translator.SourceLanguage = selectedSourceLanguage;
            translator.TargetLanguage = selectedTargetLanguage;

            OnPropertyChanged(nameof(CurrentSetHotkeys));
        }
    }
}