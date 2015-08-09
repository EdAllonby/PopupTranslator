using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using NHotkey;
using PopupTranslator.IoC;
using PopupTranslator.View;

namespace PopupTranslator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private MainWindow mainWindow;

        private IHotkeyService service;

        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Initialize(new IocConfiguration());
            service = IocKernel.Get<IHotkeyService>();

            service.HotkeyPressed += OnHotkeyPressed;

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var taskbarIcon = new TaskbarIcon
            {
                ContextMenu = new ContextMenuView(),
                Icon = PopupTranslator.Properties.Resources.TranslateLogo,
                ToolTipText = "Popup Translator"
            };

            taskbarIcon.TrayMouseDoubleClick += OnTaskbarDoubleClick;

            mainWindow = new MainWindow();
        }

        private void OnHotkeyPressed(object sender, HotkeyEventArgs e)
        {
            OpenMainWindow();
        }

        private void OnTaskbarDoubleClick(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        private void OpenMainWindow()
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