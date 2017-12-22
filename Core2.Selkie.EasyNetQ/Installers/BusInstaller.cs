using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    public class BusInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            container.Register(
                               Component.For <IBus>()
                                        .UsingFactoryMethod(() => BusBuilder.CreateMessageBus(container))
                                        .LifestyleSingleton());
        }
    }
}