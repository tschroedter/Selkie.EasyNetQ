using System.Reflection;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    public interface IRegisterMessageConsumers
    {
        void Register([NotNull] IWindsorContainer container,
                      [NotNull] Assembly assembly);
    }
}