using System.Windows;
using System.Windows.Input;

namespace PopupTranslator
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView
    {
        private readonly SettingsViewModel viewModel;

        public SettingsView()
        {
            InitializeComponent();
            viewModel = (SettingsViewModel)DataContext;
            viewModel.RequestedClose += OnRequestedClose;
        }

        private void OnRequestedClose(object sender, System.EventArgs e)
        {
            Close();
        }

        private void SettingsView_OnKeyDown(object sender, KeyEventArgs e)
        {
            ModifierKeys modifiersPressed = Keyboard.Modifiers;
            viewModel.ActionKeyPressed  = e.Key;
            viewModel.ModifierKeysPressed = modifiersPressed;
        }
    }
}