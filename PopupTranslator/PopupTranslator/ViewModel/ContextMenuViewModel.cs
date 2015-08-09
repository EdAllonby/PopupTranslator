using System.Collections.ObjectModel;
using System.Windows;
using PopupTranslator.View;

namespace PopupTranslator.ViewModel
{
    internal class ContextMenuViewModel
    {
        public ContextMenuViewModel()
        {
            Actions = new ObservableCollection<ContextItemViewModel>
            {
                new ContextItemViewModel("Settings", new RelayCommand(OpenSettings)),
                new ContextItemViewModel("Exit", new RelayCommand(Exit))
            };
        }

        public ObservableCollection<ContextItemViewModel> Actions { get; set; }

        private void OpenSettings(object obj)
        {
            var settingsMenuView = new SettingsView();
            settingsMenuView.Show();
        }

        private void Exit(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}