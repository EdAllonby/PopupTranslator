using Ninject.Modules;

namespace PopupTranslator.IoC
{
    internal sealed class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ITranslator>().To<GoogleTranslator>().InSingletonScope(); // Reuse same storage every time
            Bind<IHotkeyService>().To<HotkeyService>().InSingletonScope(); // Reuse same storage every time

            Bind<MainViewModel>().ToSelf().InTransientScope(); // Create new instance every time
            Bind<ContextMenuViewModel>().ToSelf().InTransientScope(); // Create new instance every time
            Bind<SettingsViewModel>().ToSelf().InTransientScope(); // Create new instance every time

        }
    }
}