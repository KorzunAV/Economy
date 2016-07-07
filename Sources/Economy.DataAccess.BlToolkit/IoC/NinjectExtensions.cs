using Ninject;
using Ninject.Syntax;

namespace Economy.DataAccess.BlToolkit.IoC
{
    public static class NinjectExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InstantlyCreate<T>(this IBindingNamedWithOrOnSyntax<T> binding)
        {
            binding.Kernel.Get<T>();
            return binding;
        }
    }
}
