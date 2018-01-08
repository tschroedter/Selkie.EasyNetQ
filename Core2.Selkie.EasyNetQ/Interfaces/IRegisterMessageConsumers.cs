using System.Reflection;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces
{
    public interface IRegisterMessageConsumers
    {
        [UsedImplicitly]
        void Register([NotNull] IWindsorContainer container,
                      [NotNull] Assembly assembly);
    }
}