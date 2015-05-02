using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.Installers
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class BusInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            // ReSharper disable MaximumChainedReferences
            container.Register(Component.For <IBus>()
                                        .UsingFactoryMethod(() => BusBuilder.CreateMessageBus(container))
                                        .LifestyleSingleton());
            // ReSharper restore MaximumChainedReferences
        }
    }
}