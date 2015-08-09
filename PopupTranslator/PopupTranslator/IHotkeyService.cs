using System;
using System.Windows.Input;
using NHotkey;

namespace PopupTranslator
{
    public interface IHotkeyService
    {
        ModifierKeys ModifierKeys { get; }

        Key ActionKey { get; }

        void SetNewHotkeys(Key actionKey, ModifierKeys modifierKeys);


        event EventHandler<HotkeyEventArgs> HotkeyPressed;
    }
}