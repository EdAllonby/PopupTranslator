using Ninject.Modules;

namespace PopupTranslator
{
    internal sealed class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ITranslator>().To<GoogleTranslator>().InSingletonScope(); // Reuse same storage every time

            Bind<MainViewModel>().ToSelf().InTransientScope(); // Create new instance every time
            Bind<SettingsMenuViewModel>().ToSelf().InTransientScope(); // Create new instance every time

        }
    }
}