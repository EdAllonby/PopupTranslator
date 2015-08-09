using Ninject;
using Ninject.Modules;

namespace PopupTranslator.IoC
{
    public static class IocKernel
    {
        private static StandardKernel kernel;

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }

        public static void Initialize(params INinjectModule[] modules)
        {
            if (kernel == null)
            {
                kernel = new StandardKernel(modules);
            }
        }
    }
}
