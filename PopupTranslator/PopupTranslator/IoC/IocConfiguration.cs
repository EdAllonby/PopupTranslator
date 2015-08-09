using Ninject.Modules;

namespace PopupTranslator.IoC
{
    internal sealed class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            ConfigureServices();

            ConfigureViews();
        }

        private void ConfigureServices()
        {
            Bind<ITranslator>().To<GoogleTranslator>().InSingletonScope();
            Bind<IHotkeyService>().To<HotkeyService>().InSingletonScope(); 
        }

        private void ConfigureViews()
        {
            Bind<MainViewModel>().ToSelf().InTransientScope(); 
            Bind<ContextMenuViewModel>().ToSelf().InTransientScope();
            Bind<SettingsViewModel>().ToSelf().InTransientScope();
        }
    }
}