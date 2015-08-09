namespace PopupTranslator
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();

        public ContextMenuViewModel SettingsMenuViewModel => IocKernel.Get<ContextMenuViewModel>();

    }
}