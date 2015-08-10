using System;
using System.Collections.ObjectModel;

namespace PopupTranslator.ViewModel
{
    internal class ContextMenuViewModel
    {
        public event EventHandler ExitApplicationRequested;
        public event EventHandler OpenSettingsViewRequested;

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
            var copy = OpenSettingsViewRequested;

            copy?.Invoke(this, EventArgs.Empty);
        }

        private void Exit(object obj)
        {
            var copy = ExitApplicationRequested;

            copy?.Invoke(this, EventArgs.Empty);
        }
    }
}