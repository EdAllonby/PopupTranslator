using PopupTranslator.ViewModel;

namespace PopupTranslator.IoC
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();

        public ContextMenuViewModel ContextMenuViewModel => IocKernel.Get<ContextMenuViewModel>();

        public SettingsViewModel SettingsViewModel => IocKernel.Get<SettingsViewModel>();
    }
}