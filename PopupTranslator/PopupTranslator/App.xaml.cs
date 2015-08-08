using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace PopupTranslator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly MainWindow mainWindow = new MainWindow();

        private const ModifierKeys ModifierKeys = System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Alt;
        private const Key ActionKey = Key.Down;

        protected override void OnStartup(StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            HotkeyManager.Current.AddOrReplace("Increment", ActionKey, ModifierKeys, OnIncrement);
        }

        private void OnIncrement(object o, HotkeyEventArgs e)
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
        
        public string TranslateText(string input, string languagePair)
        {
            var translation = new Translator();

            return translation.Translate(input, "English", "Chinese");
        }
    }
}