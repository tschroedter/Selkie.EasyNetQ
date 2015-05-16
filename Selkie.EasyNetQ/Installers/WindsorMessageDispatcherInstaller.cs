using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.Installers
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class WindsorMessageDispatcherInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            container.Register(
                               Component.For <AutoSubscriber>()
                                        .UsingFactoryMethod(
                                                            () =>
                                                            WindsorMessageDispatcherBuilder.CreateMessageDispatcher(
                                                                                                                    container))
                                        .LifestyleSingleton());
        }
    }
}