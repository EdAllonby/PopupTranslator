using System;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;
using PopupTranslator.Utility;

namespace PopupTranslator
{
    internal sealed class HotkeyService : IHotkeyService
    {
        private const string TranslatHotkeyName = "TranslateHotkey";

        public HotkeyService()
        {
            HotkeyManager.Current.AddOrReplace(TranslatHotkeyName, ActionKey, ModifierKeys, true, OnTranslateHotkeyPressed);
        }

        public ModifierKeys ModifierKeys { get; private set; } = ModifierKeys.Control | ModifierKeys.Alt;

        public Key ActionKey { get; private set; } = Key.Down;

        public void SetNewHotkeys(Key actionKey, ModifierKeys modifierKeys)
        {
            if (!actionKey.Equals(Key.None) && modifierKeys.Equals(ModifierKeys.None))
            {
                ActionKey = actionKey;
                ModifierKeys = modifierKeys;

                HotkeyManager.Current.AddOrReplace(TranslatHotkeyName, ActionKey, ModifierKeys, true, OnTranslateHotkeyPressed);
            }
        }

        public event EventHandler<HotkeyEventArgs> HotkeyPressed;

        private void OnTranslateHotkeyPressed(object sender, HotkeyEventArgs e)
        {
            HotkeyPressed.SafeFireEvent(this, e);
        }
    }
}