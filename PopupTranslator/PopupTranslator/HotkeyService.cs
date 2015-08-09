﻿using System;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace PopupTranslator
{
    internal sealed class HotkeyService : IHotkeyService
    {
        public HotkeyService()
        {
            HotkeyManager.Current.AddOrReplace("TranslateHotkey", ActionKey, ModifierKeys, true, OnTranslateHotkeyPressed);
        }

        public ModifierKeys ModifierKeys { get; private set; } = ModifierKeys.Control | ModifierKeys.Alt;

        public Key ActionKey { get; private set; } = Key.Down;

        public void SetNewHotkeys(Key actionKey, ModifierKeys modifierKeys)
        {
            ActionKey = actionKey;
            ModifierKeys = modifierKeys;

            HotkeyManager.Current.AddOrReplace("TranslateHotkey", ActionKey, ModifierKeys, true, OnTranslateHotkeyPressed);
        }

        public event EventHandler<HotkeyEventArgs> HotkeyPressed;

        private void OnTranslateHotkeyPressed(object sender, HotkeyEventArgs e)
        {
            var copy = HotkeyPressed;

            copy?.Invoke(this, e);
        }
    }
}