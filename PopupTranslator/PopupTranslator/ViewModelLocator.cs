namespace PopupTranslator
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();

        public SettingsMenuViewModel SettingsMenuViewModel => IocKernel.Get<SettingsMenuViewModel>();

    }
}