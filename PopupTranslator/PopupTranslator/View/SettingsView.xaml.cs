using System;
using System.Windows.Input;
using PopupTranslator.ViewModel;

namespace PopupTranslator.View
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
            viewModel = (SettingsViewModel) DataContext;
            viewModel.RequestedClose += OnRequestedClose;
        }

        private void OnRequestedClose(object sender, EventArgs e)
        {
            Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            ModifierKeys modifiersPressed = Keyboard.Modifiers;
            viewModel.ActionKeyPressed = e.Key;
            viewModel.ModifierKeysPressed = modifiersPressed;
        }
    }
}