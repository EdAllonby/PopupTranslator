using System.Collections.ObjectModel;

namespace PopupTranslator
{
    class SettingsMenuViewModel
    {
        public ObservableCollection<ContextItemViewModel> Actions { get; set; }

        public SettingsMenuViewModel()
        {
            Actions = new ObservableCollection<ContextItemViewModel>
            {
                new ContextItemViewModel("Yes", new RelayCommand(DoIt)),
                new ContextItemViewModel("No", new RelayCommand(DoIt)),
                new ContextItemViewModel("Maybe", new RelayCommand(DoIt))
            };
        }

        private void DoIt(object obj)
        {
        }
    }
}
