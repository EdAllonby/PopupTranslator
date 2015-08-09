using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using NHotkey;
using NHotkey.Wpf;

namespace PopupTranslator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private const ModifierKeys ModifierKeys = System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Alt;
        private const Key ActionKey = Key.Down;
        private readonly MainWindow mainWindow = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            TaskbarIcon taskbarIcon = new TaskbarIcon();

            taskbarIcon.ContextMenu = new SettingsMenuView();

            HotkeyManager.Current.AddOrReplace("Increment", ActionKey, ModifierKeys, OnIncrement);
        }

        private void OnIncrement(object sender, HotkeyEventArgs eventArgs)
        {
            if (!mainWindow.IsActive)
            {
                mainWindow.Show();
            }
            else
            {
                mainWindow.Hide();
            }
        }
    }
}