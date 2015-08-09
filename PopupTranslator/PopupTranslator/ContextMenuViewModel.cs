using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PopupTranslator
{
    class ContextMenuViewModel
    {
        public ObservableCollection<ContextItemViewModel> Actions { get; set; }

        public ContextMenuViewModel()
        {
            Actions = new ObservableCollection<ContextItemViewModel>
            {
                new ContextItemViewModel("Settings", new RelayCommand(OpenSettings)),
                new ContextItemViewModel("Exit", new RelayCommand(Exit))
            };
        }

        private void OpenSettings(object obj)
        {
            SettingsView settingsMenuView = new SettingsView();
            settingsMenuView.Show();
        }

        private void Exit(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
