﻿using System;
using System.Drawing;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            taskbarIcon.TrayMouseDoubleClick += TaskbarIcon_TrayMouseDoubleClick;

            mainWindow = new MainWindow();
        }

        public static ImageSource ConvertToImageSource(Icon icon)
        {
            return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void OnHotkeyPressed(object sender, HotkeyEventArgs e)
        {
            OpenMainWindow();
        }

        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
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