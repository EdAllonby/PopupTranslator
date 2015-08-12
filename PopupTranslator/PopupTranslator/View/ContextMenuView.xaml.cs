using System;
using System.Windows;
using PopupTranslator.ViewModel;

namespace PopupTranslator.View
{
    /// <summary>
    /// Interaction logic for SettingsMenuView.xaml
    /// </summary>
    public partial class ContextMenuView
    {
        public ContextMenuView()
        {
            InitializeComponent();

            var viewModel = (ContextMenuViewModel) DataContext;
            viewModel.OpenSettingsViewRequested += OnOpenSettingsViewRequested;
            viewModel.ExitApplicationRequested += OnExitApplicationRequested;
        }

        private static void OnExitApplicationRequested(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private static void OnOpenSettingsViewRequested(object sender, EventArgs e)
        {
            var settingsView = new SettingsView();
            settingsView.Show();
        }
    }
}