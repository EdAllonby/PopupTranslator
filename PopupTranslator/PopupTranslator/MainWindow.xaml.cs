using System;
using System.Windows;
using System.Windows.Input;

namespace PopupTranslator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            try
            {


            InitializeComponent();
            }
            catch (Exception)
            {

                throw;
            }
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Rect desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
        }
    }
}