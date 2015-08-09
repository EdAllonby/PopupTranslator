using System.Collections.ObjectModel;
using System.Net.Mime;
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
            throw new System.NotImplementedException();
        }

        private void Exit(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
